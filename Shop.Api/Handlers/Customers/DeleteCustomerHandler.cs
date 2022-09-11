using MediatR;
using Shop.Api.Commands.Customers;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Customers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private ICustomerService _customerService;

        public DeleteCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerService.DeleteCustomer(request.Id);
        }
    }
}
