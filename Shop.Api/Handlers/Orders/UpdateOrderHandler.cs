using MediatR;
using Shop.Api.Commands.Orders;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Orders
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponseModel?>
    {
        private IOrderService _customerService;

        public UpdateOrderHandler(IOrderService customerService)
        {
            _customerService = customerService;
        }

        public async Task<OrderResponseModel?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = new()
            {
                CustomerId = request.Order.CustomerId,
                Items = request.Order.Items.Select(i => new Item()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                }).ToList()
            };

            order = await _customerService.UpdateOrder(request.Id, order);

            if (order is null)
                return null;

            OrderResponseModel orderResponse = new()
            {
                Id = order.Id,
                Customer = new CustomerResponseModel()
                {
                    Id = order.Customer.Id,
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    Address = order.Customer.Address,
                    PostalCode = order.Customer.PostalCode
                },
                DocDate = order.DocDate,
                DocTotal = order.DocTotal,
                Items = order.Items.Select(i => new ItemResponseModel()
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
