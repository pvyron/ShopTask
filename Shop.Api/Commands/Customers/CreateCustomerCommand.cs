using MediatR;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Commands.Customers
{
    public class CreateCustomerCommand : IRequest<CustomerResponseModel?>
    {
        public CustomerRequestModel Customer { get; private set; }

        public CreateCustomerCommand(CustomerRequestModel customer)
        {
            Customer = customer;
        }
    }
}
