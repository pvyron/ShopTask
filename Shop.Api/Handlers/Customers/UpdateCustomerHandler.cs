using MediatR;
using Shop.Api.Commands.Customers;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Customers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponseModel?>
    {
        private ICustomerService _customerService;

        public UpdateCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerResponseModel?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = new()
            {
                Id = request.Id,
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Address = request.Customer.Address,
                PostalCode = request.Customer.PostalCode
            };

            customer = await _customerService.UpdateCustomer(customer);

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
