using MediatR;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Orders;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Orders
{
    public class GetOrdersForCustomerHandler : IRequestHandler<GetOrdersForCustomerQuery, List<OrderResponseModel>>
    {
        public IOrderService _orderService { get; }

        public GetOrdersForCustomerHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<OrderResponseModel>> Handle(GetOrdersForCustomerQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderService.GetOrdersOfCustomer(request.Id);

            List<OrderResponseModel> orderResponses = orders.Select(o => new OrderResponseModel()
            {
                Id = o.Id,
                Customer = new CustomerResponseModel()
                {
                    Id = o.Customer.Id,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    Address = o.Customer.Address,
                    PostalCode = o.Customer.PostalCode
                },
                DocDate = o.DocDate,
                DocTotal = o.DocTotal,
                Items = o.Items.Select(i => new ItemResponseModel()
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
            }).ToList();

            return orderResponses;
        }
    }
}
