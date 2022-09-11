using MediatR;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<CustomerResponseModel?>
    {
        public Guid Id { get; }
        public CustomerRequestModel Customer { get; }

        public UpdateCustomerCommand(Guid id, CustomerRequestModel customer)
        {
            Id = id;
            Customer = customer;
        }
    }
}
