using MediatR;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Orders;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Orders
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderResponseModel?>
    {
        private IOrderService _orderService;

        public GetOrderByIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderResponseModel?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order? order = await _orderService.GetOrder(request.Id);

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
