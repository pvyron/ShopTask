using MediatR;
using Shop.Api.Models.DbModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Customers;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Customers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponseModel>>
    {
        private ICustomerService _customerService;

        public GetAllCustomersHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<List<CustomerResponseModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            List<Customer> customers = await _customerService.GetAllCustomers();

            List<CustomerResponseModel> customersResponse = customers.Select(c => new CustomerResponseModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                PostalCode = c.PostalCode
            }).ToList();

            return customersResponse;
        }
    }
}
