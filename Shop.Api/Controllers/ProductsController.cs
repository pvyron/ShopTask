using Microsoft.AspNetCore.Mvc;
using Shop.Api.Models.DbModels;
using Shop.Api.Services;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productSerice;

        public ProductsController(IProductService productSerice)
        {
            _productSerice = productSerice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> products = await _productSerice.GetAllProducts();

            if (products.Count == 0)
                return NoContent();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Product? product = await _productSerice.GetProduct(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            Product? newProduct = await _productSerice.CreateProduct(product);

            if (newProduct is null)
                return BadRequest(product);

            return Ok(newProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            Product? newProduct = await _productSerice.UpdateProduct(product);

            if (newProduct is null)
                return BadRequest(product);

            return Ok(newProduct);
        }
    }
}
