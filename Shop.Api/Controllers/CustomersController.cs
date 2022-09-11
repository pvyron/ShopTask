using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Commands.Customers;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;
using Shop.Api.Queries.Customers;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all customers from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CustomerResponseModel> customers = await _mediator.Send(new GetAllCustomersQuery());

            if (customers.Count == 0)
                return NoContent();

            return Ok(customers);
        }

        /// <summary>
        /// Gets the corresponding customer to the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique guid for the requested customer</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            CustomerResponseModel? customer = await _mediator.Send(new GetCustomerByIdQuery(id));

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Creates customer and returns it with the generated id
        /// </summary>
        /// <param name="customer">Model for customer creation</param>
        /// <returns>400 if creation fails</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerRequestModel customer)
        {
            CustomerResponseModel? newCustomer = await _mediator.Send(new CreateCustomerCommand(customer));

            if (newCustomer is null)
                return BadRequest(customer);

            return Ok(newCustomer);
        }

        /// <summary>
        /// Updates the customer which corresponds to the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <param name="customer">Model for customer update</param>
        /// <returns>400 if the update fails</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CustomerRequestModel customer)
        {
            CustomerResponseModel? newCustomer = await _mediator.Send(new UpdateCustomerCommand(id, customer));

            if (newCustomer is null)
                return BadRequest(customer);

            return Ok(newCustomer);
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="id">The id of the customer to be deleted</param>
        /// <returns>400 if the delete fails</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _mediator.Send(new DeleteCustomerCommand(id)))
                return BadRequest();

            return Ok();
        }
    }
}
