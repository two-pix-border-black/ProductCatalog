using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Factories;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Data.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }

        #region CREATE
        // Create a new category
        public async Task<Category> CreateCategory(CreateCategoryDTO categoryDto)
        {
            if (categoryDto.ParentCategoryId == 0)
            {
                categoryDto.ParentCategoryId = null;
            }
            else
            {
                var parentCategory = await _context.Categories.FindAsync(categoryDto.ParentCategoryId);
                if (parentCategory == null)
                {
                    throw new ArgumentException("Invalid ParentCategoryId");
                }
            }

            var category = CategoryFactory.CreateCategory(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        #endregion

        #region READ
        // Get all categories and subcategories
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories
                                           .Where(c => c.ParentCategoryId == null)
                                           .Include(c => c.ChildCategories)
                                           .ThenInclude(c => c.ChildCategories)
                                           .ToListAsync();

            return categories.Select(CategoryFactory.ToCategoryDTO).ToList();
        }

        // Get a category by ID
        public async Task<CategoryContentDTO> GetCategory(int id)
        {
            var category = await _context.Categories
                                         .Include(c => c.Products)
                                         .Include(c => c.ChildCategories)
                                         .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }

            return CategoryFactory.ToCategoryContentDTO(category);
        }

        // Get a category by name
        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.CategoryName == name);
        }

        public async Task<Category> GetCategoryEntity(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }
        #endregion

        #region UPDATE
        // Update a category
        public async Task<Category> UpdateCategory(int id, CreateCategoryDTO categoryDto)
        {
            var category = await GetCategoryEntity(id);
            if (category == null)
            {
                return null;
            }

            category = CategoryFactory.UpdateCategory(category, categoryDto.CategoryName, categoryDto.ParentCategoryId);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
        #endregion

        #region DELETE
        // Delete a category
        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
