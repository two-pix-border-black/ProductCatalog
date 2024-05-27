using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Api.Controllers;
using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Factories;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;
using Xunit;

namespace ProductCatalog.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductRepo> _mockRepo;

        public ProductsControllerTests()
        {
            _mockRepo = new Mock<IProductRepo>();
            _controller = new ProductsController(_mockRepo.Object);
        }

        // Test the create
        [Fact]
        public async Task CreateProductTest()
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

            var createdProduct = ProductFactory.FactoryCreateProduct(createProductDto);
            createdProduct.ProductId = 1;

            _mockRepo.Setup(repo => repo.CreateProduct(createProductDto)).ReturnsAsync(createdProduct);

            var result = await _controller.CreateProduct(createProductDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Product>(createdResult.Value);
            Assert.Equal(1, returnValue.ProductId);
            Assert.Equal("New Product", returnValue.Name);
        }

        // Test get products
        [Fact]
        public async Task GetProducts_WhenProductsExist()
        {
            var products = new List<ProductDetailsDTO>
            {
                new ProductDetailsDTO
                {
                ProductId = 1,
                Name = "Test Product",
                Description = "Description",
                Color = "Red",
                Size = "M",
                ImageUrl = "http://test.com/image.png",
                Price = 99.99m,
                Quantity = 10,
                Gender = "Unisex",
                CategoryId = 1,
                CategoryName = "CategoryName",
                IsAvailable = true
                }
            };

            _mockRepo.Setup(repo => repo.GetProducts()).ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductDetailsDTO>>(okResult.Value);
            Assert.Single(returnValue);
            var productDto = returnValue.First();
            Assert.Equal(1, productDto.ProductId);
            Assert.Equal("Test Product", productDto.Name);
        }

        // Test products if none exist
        [Fact]
        public async Task GetProducts_WhenNoProductsExist()
        {
            _mockRepo.Setup(repo => repo.GetProducts()).ReturnsAsync(new List<ProductDetailsDTO>());

            var result = await _controller.GetProducts();

            Assert.IsType<NotFoundResult>(result);
        }

        // Test to update product
        [Fact]
        public async Task UpdateProductTest()
        {
            var updateProductDto = new UpdateProductDTO
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Color = "Green",
                Size = "S",
                ImageUrl = "http://test.com/image.png",
                Price = 89.99m,
                Quantity = 15,
                Gender = "Female",
                CategoryId = 3,
                IsAvailable = true
            };

            var existingProduct = new Product
            {
                ProductId = 1,
                Name = "Old Product",
                Description = "Old Description",
                Color = "Blue",
                Size = "L",
                ImageUrl = "http://test.com/oldimage.png",
                Price = 79.99m,
                Quantity = 5,
                Gender = "Male",
                CategoryId = 2,
                IsAvailable = false
            };

            _mockRepo.Setup(repo => repo.UpdateProduct(existingProduct.ProductId, updateProductDto)).ReturnsAsync(existingProduct);

            var result = await _controller.UpdateProduct(existingProduct.ProductId, updateProductDto);

            Assert.IsType<NoContentResult>(result);
        }

        // Test to update product none exist
        [Fact]
        public async Task UpdateProductWhenNoneExist()
        {
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

            _mockRepo.Setup(repo => repo.UpdateProduct(It.IsAny<int>(), updateProductDto)).ReturnsAsync((Product)null);

            var result = await _controller.UpdateProduct(1, updateProductDto);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
