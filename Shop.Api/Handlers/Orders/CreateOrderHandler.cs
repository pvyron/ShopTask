using MediatR;
using Shop.Api.Commands.Orders;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponseModel?>
    {
        private IOrderService _orderService;

        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderResponseModel?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order? newOrder = new()
            {
                CustomerId = request.Order.CustomerId,
                Items = request.Order.Items.Select(i => new Item()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                }).ToList()
            };

            newOrder = await _orderService.CreateOrder(newOrder);

            if (newOrder is null)
                return null;

            OrderResponseModel orderResponse = new()
            {
                Id = newOrder.Id,
                Customer = new CustomerResponseModel()
                {
                    Id = newOrder.Customer.Id,
                    FirstName = newOrder.Customer.FirstName,
                    LastName = newOrder.Customer.LastName,
                    Address = newOrder.Customer.Address,
                    PostalCode = newOrder.Customer.PostalCode
                },
                DocDate = newOrder.DocDate,
                DocTotal = newOrder.DocTotal,
                Items = newOrder.Items.Select(i => new ItemResponseModel()
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    Product = new ProductResponseModel()
                    {
                        Id = i.Product.Id,
                        Name = i.Product.Name,
                        Price = i.Product.Price
                    }
                }).ToList()
            };

            return orderResponse;
        }
    }
}
