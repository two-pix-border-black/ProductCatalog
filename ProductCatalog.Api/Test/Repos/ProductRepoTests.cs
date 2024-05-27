using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;
using ProductCatalog.Api.Data.Repo;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;
using Xunit;

namespace ProductCatalog.Tests.Repositories
{
    public class ProductRepoTests
    {
        private readonly ProductRepo _repo;
        private readonly AppDbContext _context;

        public ProductRepoTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestDatabase")
                 .Options;

            _context = new AppDbContext(options);
            _repo = new ProductRepo(_context);
        }

        // Test the create
        [Fact]
        public async Task CreateProductDbTest()
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

            var result = await _repo.CreateProduct(createProductDto);

            var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == result.ProductId);
            Assert.NotNull(productInDb);
            Assert.Equal("New Product", productInDb.Name);
            Assert.Equal("Description", productInDb.Description);
            Assert.Equal("Red", productInDb.Color);
            Assert.Equal("M", productInDb.Size);
            Assert.Equal("http://test.com/image.png", productInDb.ImageUrl);
            Assert.Equal(99.99m, productInDb.Price);
            Assert.Equal(10, productInDb.Quantity);
            Assert.Equal("Unisex", productInDb.Gender);
            Assert.Equal(1, productInDb.CategoryId);
            Assert.True(productInDb.IsAvailable);
        }

        // Test to get products
        [Fact]
        public async Task GetProductsRepoTest()
        {
            var products = new List<Product>
            {
                new Product
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
                IsAvailable = true,
                Category = new Category { CategoryId = 1, CategoryName = "CategoryName" }
                }
            };

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            var result = await _repo.GetProducts();

            Assert.NotNull(result);
            Assert.Single(result);
            var productDto = result.First();
            Assert.Equal(1, productDto.ProductId);
            Assert.Equal("Test Product", productDto.Name);
        }

        // Test for the update product
        [Fact]
        public async Task UpdateProductDbTest()
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

            await _context.Products.AddAsync(existingProduct);
            await _context.SaveChangesAsync();

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

            var result = await _repo.UpdateProduct(existingProduct.ProductId, updateProductDto);

            var updatedProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == existingProduct.ProductId);
            Assert.NotNull(updatedProduct);
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
