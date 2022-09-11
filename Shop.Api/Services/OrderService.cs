using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                List<Order> orders = (await _unitOfWork.Orders.GetAllAsync()).ToList();

                foreach (Order order in orders)
                {
                    Customer? customer = await _unitOfWork.Customers.GetByIdAsync(order.CustomerId);

                    //Should never throw
                    if (customer is null)
                        throw new Exception($"Customer with id {order.CustomerId} no longer exists");

                    order.Customer = customer;
                    order.Items = await GetItemsOfOrderAsync(order.Id);
                }

                return orders;
            }
            catch
            {
                return new List<Order>();
            }
        }

        public async Task<Order?> GetOrder(Guid id)
        {
            try
            {
                Order? order = await _unitOfWork.Orders.GetByIdAsync(id);

                if (order == null)
                    throw new Exception($"Invalid order id {id}");

                Customer? customer = await _unitOfWork.Customers.GetByIdAsync(order.CustomerId);

                //Should never throw
                if (customer is null)
                    throw new Exception($"Customer with id {order.CustomerId} no longer exists");

                order.Customer = customer;
                order.Items = await GetItemsOfOrderAsync(id);

                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Order?> CreateOrder(Order createOrder)
        {
            try
            {
                Customer? customer = await _unitOfWork.Customers.GetByIdAsync(createOrder.CustomerId);

                if (customer is null)
                    throw new Exception($"Invalid customer id {createOrder.CustomerId}");

                List<Item> items = new();

                foreach (Item item in createOrder.Items)
                {
                    Product? product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);

                    if (product is null)
                        throw new Exception($"Invalid product id {item.ProductId}");

                    if (item.Quantity <= 0)
                        throw new Exception($"Invalid quantity {item.Quantity} for product {product.Name}");

                    items.Add(new Item()
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                }

                Order order = new()
                {
                    Customer = customer,
                    DocDate = DateTime.Now,
                    DocTotal = items.Sum(i => i.Product.Price * i.Quantity),
                    Items = items
                };

                Order newOrder = await _unitOfWork.Orders.CreateAsync(order);

                await _unitOfWork.CompleteAsync();

                return newOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order?> UpdateOrder(Guid id, Order createOrder)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                Order? orderToUpdate = await GetOrder(id);

                if (orderToUpdate is null)
                    throw new Exception($"Invalid order id {id}");

                Customer? customer = await _unitOfWork.Customers.GetByIdAsync(createOrder.CustomerId);

                if (customer is null)
                    throw new Exception($"Invalid customer id {createOrder.CustomerId}");

                foreach (Item item in await _unitOfWork.Items.GetItemsOfOrder(id))
                {
                    await _unitOfWork.Items.DeleteAsync(item.Id);
                }

                List<Item> items = new();

                foreach (Item item in createOrder.Items)
                {
                    Product? product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);

                    if (product is null)
                        throw new Exception($"Invalid product id {item.ProductId}");

                    if (item.Quantity <= 0)
                        throw new Exception($"Invalid quantity {item.Quantity} for product {product.Name}");

                    items.Add(new Item()
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                }

                orderToUpdate.Customer = customer;
                orderToUpdate.DocTotal = items.Sum(i => i.Product.Price * i.Quantity);
                orderToUpdate.Items = items;

                Order? updatedOrder = await _unitOfWork.Orders.UpdateAsync(orderToUpdate);

                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitTransaction();

                return updatedOrder;
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();

                return null;
            }
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                List<Item> items = (await _unitOfWork.Items.GetItemsOfOrder(id)).ToList();

                foreach (Item item in items)
                {
                    await _unitOfWork.Items.DeleteAsync(item.Id);
                }

                await _unitOfWork.Orders.DeleteAsync(id);

                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitTransaction();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction();

                return false;
            }
        }

        public async Task<List<Order>> GetOrdersOfCustomer(Guid customerId)
        {
            List<Order> orders = (await _unitOfWork.Orders.GetOrdersOfCustomer(customerId)).ToList();

            foreach (Order order in orders)
            {
                Customer? customer = await _unitOfWork.Customers.GetByIdAsync(order.CustomerId);

                //Should never throw
                if (customer is null)
                    throw new Exception($"Customer with id {order.CustomerId} no longer exists");

                order.Customer = customer;
                order.Items = await GetItemsOfOrderAsync(order.Id);
            }

            return orders;
        }

        async Task<List<Item>> GetItemsOfOrderAsync(Guid orderId)
        {
            List<Item> items = (await _unitOfWork.Items.GetItemsOfOrder(orderId)).ToList();

            foreach (Item item in items)
            {
                Product? product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);

                //Should never throw
                if (product is null)
                    throw new Exception($"Product with id {item.ProductId} no longer exists");

                item.Product = product;
            }

            return items;
        }
    }
}
