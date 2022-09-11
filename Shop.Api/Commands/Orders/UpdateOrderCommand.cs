using MediatR;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Commands.Orders
{
    public class UpdateOrderCommand : IRequest<OrderResponseModel?>
    {
        public Guid Id { get; }
        public OrderRequestModel Order { get; }

        public UpdateOrderCommand(Guid id, OrderRequestModel order)
        {
            Id = id;
            Order = order;
        }
    }
}
