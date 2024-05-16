using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Data.Interface
{
    public interface ICategoryRepo
    {
        public Task<Category> CreateCategory(CreateCategoryDTO categoryDto);
        public Task DeleteCategory(Category category);
        public Task<List<CategoryDTO>> GetCategories();
        public Task<CategoryContentDTO> GetCategory(int id);
        public Task<Category> GetCategoryByName(string name);
        public Task<Category> UpdateCategory(int id, CreateCategoryDTO categoryDto);
        public Task<Category> GetCategoryEntity(int id);
    }
}
