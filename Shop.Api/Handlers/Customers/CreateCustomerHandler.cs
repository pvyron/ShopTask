using MediatR;
using Shop.Api.Commands.Customers;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Customers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResponseModel?>
    {
        private ICustomerService _customerService;

        public CreateCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerResponseModel?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = new()
            {
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Address = request.Customer.Address,
                PostalCode = request.Customer.PostalCode
            };

            customer = await _customerService.CreateCustomer(customer);

            if (customer is null)
                return null;

            CustomerResponseModel customerResponse = new()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                PostalCode = customer.PostalCode
            };

            return customerResponse;
        }
    }
}
