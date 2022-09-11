using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Commands.Orders;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Orders;
using Shop.Api.Services;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IOrderService orderService, IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all orders from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<OrderResponseModel> orders = await _mediator.Send(new GetAllOrdersQuery());

            if (orders.Count == 0)
                return NoContent();

            return Ok(orders);
        }

        /// <summary>
        /// Gets the corresponding order to the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id for the requested order</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            OrderResponseModel? order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order is null)
                return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Gets all the orders of the customer with the provided <paramref name="customerId"/>
        /// sorted by the orders' date
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>204 if the customer has no orders or doesn't exist</returns>
        [HttpGet("OfCustomer/{customerId}")]
        public async Task<IActionResult> GetOrdersForCustomer(Guid customerId)
        {
            List<OrderResponseModel> orders = await _mediator.Send(new GetOrdersForCustomerQuery(customerId));

            if (orders.Count == 0)
                return NoContent();

            return Ok(orders);
        }

        /// <summary>
        /// Creates an order and returns it with the generated id
        /// </summary>
        /// <param name="order">Model for order creation</param>
        /// <returns>400 if creation fails</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequestModel order)
        {
            OrderResponseModel? newOrder = await _mediator.Send(new CreateOrderCommand(order));

            if (newOrder is null)
                return BadRequest(order);

            return Ok(newOrder);
        }

        /// <summary>
        /// Updates the order which corresponds to the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Order's id</param>
        /// <param name="order">Model for order update</param>
        /// <returns>400 if the update fails</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OrderRequestModel order)
        {
            OrderResponseModel? newOrder = await _mediator.Send(new UpdateOrderCommand(id, order));

            if (newOrder is null)
                return BadRequest(order);

            return Ok(newOrder);
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="id">The id of the order to be deleted</param>
        /// <returns>400 if the delete fails</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _mediator.Send(new DeleteOrderCommand(id)))
                return BadRequest();

            return Ok();
        }
    }
}
