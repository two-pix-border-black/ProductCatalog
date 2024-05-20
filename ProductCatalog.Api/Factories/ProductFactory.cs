using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Factories
{
    public class ProductFactory
    {
        // Factory method to create a product
        public static Product FactoryCreateProduct(CreateProductDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Color = dto.Color,
                Size = dto.Size,
                ImageUrl = dto.ImageUrl,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Gender = dto.Gender,
                CategoryId = dto.CategoryId,
                IsAvailable = true
            };
        }

        // Factory method to update a product
        public static Product FactoryUpdateProduct(Product product, UpdateProductDTO dto)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Color = dto.Color;
            product.Size = dto.Size;
            product.ImageUrl = dto.ImageUrl;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.Gender = dto.Gender;
            product.CategoryId = dto.CategoryId;
            product.IsAvailable = dto.IsAvailable;

            return product;
        }

        // Factory method to get a product by ID
        public static ProductDetailsDTO FactoryGetProductById(Product product)
        {
            return new ProductDetailsDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Color = product.Color,
                Size = product.Size,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                IsAvailable = product.IsAvailable
            };
        }

        // Factory method to get a product list
        public static ProductDTO FactoryGetProductList(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable
            };
        }

    }
}
