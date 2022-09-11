using MediatR;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Queries.Orders
{
    public class GetOrdersForCustomerQuery : IRequest<List<OrderResponseModel>>
    {
        public Guid Id { get; }

        public GetOrdersForCustomerQuery(Guid id)
        {
            Id = id;
        }
    }
}
