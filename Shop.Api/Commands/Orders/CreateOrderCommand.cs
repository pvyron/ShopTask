using MediatR;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;

namespace Shop.Api.Commands.Orders
{
    public class CreateOrderCommand : IRequest<OrderResponseModel?>
    {
        public OrderRequestModel Order { get; private set; }

        public CreateOrderCommand(OrderRequestModel order)
        {
            Order = order;
        }
    }
}
