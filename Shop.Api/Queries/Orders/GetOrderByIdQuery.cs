using MediatR;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Queries.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderResponseModel?>
    {
        public Guid Id { get; private set; }

        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
