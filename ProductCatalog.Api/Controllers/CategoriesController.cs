using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _repo;

        public CategoriesController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        #region CREATE
        // Create
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDto)
        {
            if (categoryDto.ParentCategoryId != null && categoryDto.ParentCategoryId != 0)
            {
                var parentCategory = await _repo.GetCategory((int)categoryDto.ParentCategoryId);
                if (parentCategory == null)
                {
                    return BadRequest("Invalid ParentCategoryId");
                }
            }

            var newCategory = await _repo.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = newCategory.CategoryId }, newCategory);
        }
        #endregion

        #region READ
        // Get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repo.GetCategories();
            return Ok(categories);
        }

        // Get category by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _repo.GetCategory(id);
            if (category == null)
            {
                return NotFound("No category with that ID");
            }
            return Ok(category);
        }
        #endregion

        #region UPDATE
        // Update
        [HttpPut("update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDTO categoryDto)
        {
            var updatedCategory = await _repo.UpdateCategory(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound("No category with that ID");
            }

            return NoContent();
        }
        #endregion

        #region DELETE
        // Delete
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repo.GetCategoryEntity(id);
            if (category == null)
            {
                return NotFound("No category with that ID");
            }

            await _repo.DeleteCategory(category);
            return NoContent();
        }
        #endregion
    }
}
