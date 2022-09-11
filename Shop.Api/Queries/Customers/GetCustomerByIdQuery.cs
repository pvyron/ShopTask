using MediatR;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Queries.Customers
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponseModel?>
    {
        public Guid Id { get; private set; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
