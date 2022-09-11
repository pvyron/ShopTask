using MediatR;
using Shop.Api.Commands.Orders;
using Shop.Api.Services;

namespace Shop.Api.Handlers.Orders
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private IOrderService _orderService;

        public DeleteOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.DeleteOrder(request.Id);
        }
    }
}
