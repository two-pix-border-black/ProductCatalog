using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Factories
{
    public class CategoryFactory
    {
        // Factory method to create a new category
        public static Category CreateCategory(CreateCategoryDTO categoryDto)
        {
            return new Category
            {
                CategoryName = categoryDto.CategoryName,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
        }

        // Factory method to update a category
        public static Category UpdateCategory(Category category, string categoryName, int? parentCategoryId)
        {
            category.CategoryName = categoryName;
            category.ParentCategoryId = parentCategoryId;

            return category;
        }

        // Factory method to get all categories and convert to DTO
        public static CategoryDTO GetCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ChildCategories = category.ChildCategories.Select(c => new ChildCategoryDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).ToList()
            };
        }

        // Factory method to get all categories and convert to DTO without products
        public static CategoryDTO ToCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ChildCategories = category.ChildCategories.Select(ToChildCategoryDTO).ToList()
            };
        }
        // Factory method to get a subcategories from parent category and convert to DTO
        public static ChildCategoryDTO ToChildCategoryDTO(Category category)
        {
            return new ChildCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ChildCategories = category.ChildCategories.Select(ToChildCategoryDTO).ToList()
            };
        }

        // Factory method to get a category with products and convert to DTO
        public static CategoryContentDTO ToCategoryContentDTO(Category category)
        {
            return new CategoryContentDTO
            {
                CategoryName = category.CategoryName,
                Products = category.Products.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IsAvailable = p.IsAvailable
                }).ToList()
            };
        }
    }
}
