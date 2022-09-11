using MediatR;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Queries.Customers
{
    public class GetAllCustomersQuery : IRequest<List<CustomerResponseModel>>
    {
    }
}
