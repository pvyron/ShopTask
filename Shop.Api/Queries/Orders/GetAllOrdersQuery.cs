using MediatR;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Queries.Orders
{
    public class GetAllOrdersQuery : IRequest<List<OrderResponseModel>>
    {
    }
}
