using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Factories;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Data.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        #region CREATE
        // Create a new product
        public async Task<Product> CreateProduct(CreateProductDTO dto)
        {
            var product = ProductFactory.FactoryCreateProduct(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        #endregion

        #region READ
        // Get product by ID
        public async Task<ProductDetailsDTO> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }

            return ProductFactory.FactoryGetProductById(product);
        }

        // Get product by name
        public async Task<List<ProductDetailsDTO>> GetProductByName(string name)
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .Where(p => p.Name.Contains(name))
                                         .ToListAsync();
            return products.Select(ProductFactory.FactoryGetProductList).ToList();
        }

        // Get all products
        public async Task<List<ProductDetailsDTO>> GetProducts()
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .ToListAsync();
            return products.Select(ProductFactory.FactoryGetProductList).ToList();
        }

        // Get unique colors
        public async Task<List<string>> GetUniqueColors()
        {
            return await _context.Products
                .Select(p => p.Color)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Product> GetProductId(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }
        #endregion

        #region UPDATE
        // Update a product
        public async Task<Product> UpdateProduct(int id, UpdateProductDTO dto)
        {
            var product = await GetProductId(id);
            if (product == null)
            {
                return null;
            }

            product = ProductFactory.FactoryUpdateProduct(product, dto);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
        #endregion

        #region DELETE
        // Delete a product
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await GetProductId(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
