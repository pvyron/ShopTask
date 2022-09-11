using MediatR;

namespace Shop.Api.Commands.Orders
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; }
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
