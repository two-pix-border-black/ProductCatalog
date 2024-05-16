using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repo;

        public ProductsController(IProductRepo repo)
        {
            _repo = repo;
        }

        #region CREATE
        // Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            var newProduct = await _repo.CreateProduct(dto);
            if (newProduct == null)
            {
                return BadRequest("Unable to create product.");
            }
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
        }
        #endregion

        #region READ
        // Get all products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // Get product by name
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _repo.GetProductByName(name);
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        // Get product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repo.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        #endregion

        #region UPDATE
        // Update
        [HttpPut("put/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDto)
        {
            var updatedProduct = await _repo.UpdateProduct(id, productDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion

        #region DELETE
        // Delete
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _repo.DeleteProduct(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
