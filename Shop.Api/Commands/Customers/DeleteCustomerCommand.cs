using MediatR;

namespace Shop.Api.Commands.Customers
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; private set; }

        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
    }
}
