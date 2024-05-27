using ProductCatalog.Api.Factories;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;
using Xunit;

namespace ProductCatalog.Tests.Factories
{
    public class ProductFactoryTests
    {
        // Test for create
        [Fact]
        public void FactoryCreateProductTest()
        {
            var createProductDto = new CreateProductDTO
            {
                Name = "New Product",
                Description = "Description",
                Color = "Red",
                Size = "M",
                ImageUrl = "http://test.com/image.png",
                Price = 99.99m,
                Quantity = 10,
                Gender = "Unisex",
                CategoryId = 1
            };

            var product = ProductFactory.FactoryCreateProduct(createProductDto);

            Assert.Equal("New Product", product.Name);
            Assert.Equal("Description", product.Description);
            Assert.Equal("Red", product.Color);
            Assert.Equal("M", product.Size);
            Assert.Equal("http://test.com/image.png", product.ImageUrl);
            Assert.Equal(99.99m, product.Price);
            Assert.Equal(10, product.Quantity);
            Assert.Equal("Unisex", product.Gender);
            Assert.Equal(1, product.CategoryId);
            Assert.True(product.IsAvailable);
        }

        // Test to get products
        [Fact]
        public void FactoryGetProductListTest()
        {
            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Description = "Description",
                Color = "Red",
                Size = "M",
                ImageUrl = "http://example.com/image.png",
                Price = 99.99m,
                Quantity = 10,
                Gender = "Unisex",
                CategoryId = 1,
                IsAvailable = true,
                Category = new Category { CategoryId = 1, CategoryName = "CategoryName" }
            };

            var result = ProductFactory.FactoryGetProductList(product);

            Assert.Equal(1, result.ProductId);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal("Description", result.Description);
            Assert.Equal("Red", result.Color);
            Assert.Equal("M", result.Size);
            Assert.Equal("http://example.com/image.png", result.ImageUrl);
            Assert.Equal(99.99m, result.Price);
            Assert.Equal(10, result.Quantity);
            Assert.Equal("Unisex", result.Gender);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("CategoryName", result.CategoryName);
            Assert.True(result.IsAvailable);
        }

        // Test for update product
        [Fact]
        public void FactoryUpdateProductTest()
        {
            var existingProduct = new Product
            {
                ProductId = 1,
                Name = "Old Product",
                Description = "Old Description",
                Color = "Blue",
                Size = "L",
                ImageUrl = "http://test.com/image.png",
                Price = 79.99m,
                Quantity = 5,
                Gender = "Male",
                CategoryId = 2,
                IsAvailable = false
            };

            var updateProductDto = new UpdateProductDTO
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Color = "Green",
                Size = "S",
                ImageUrl = "http://test.com/updatedImage.png",
                Price = 89.99m,
                Quantity = 15,
                Gender = "Female",
                CategoryId = 3,
                IsAvailable = true
            };

            var updatedProduct = ProductFactory.FactoryUpdateProduct(existingProduct, updateProductDto);

            Assert.Equal("Updated Product", updatedProduct.Name);
            Assert.Equal("Updated Description", updatedProduct.Description);
            Assert.Equal("Green", updatedProduct.Color);
            Assert.Equal("S", updatedProduct.Size);
            Assert.Equal("http://test.com/updatedImage.png", updatedProduct.ImageUrl);
            Assert.Equal(89.99m, updatedProduct.Price);
            Assert.Equal(15, updatedProduct.Quantity);
            Assert.Equal("Female", updatedProduct.Gender);
            Assert.Equal(3, updatedProduct.CategoryId);
            Assert.True(updatedProduct.IsAvailable);
        }

    }
}
