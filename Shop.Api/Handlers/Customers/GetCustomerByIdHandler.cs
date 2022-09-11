using MediatR;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Customers;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Customers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponseModel?>
    {
        private ICustomerService _customerService;

        public GetCustomerByIdHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerResponseModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerService.GetCustomer(request.Id);

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
