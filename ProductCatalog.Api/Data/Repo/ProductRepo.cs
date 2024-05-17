﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        // Get product by name
        public async Task<List<ProductDTO>> GetProductByName(string name)
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .Where(p => p.Name.Contains(name))
                                         .ToListAsync();
            return products.Select(ProductFactory.FactoryGetProductList).ToList();
        }

        // Get all products
        public async Task<List<ProductDTO>> GetProducts()
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .ToListAsync();
            return products.Select(ProductFactory.FactoryGetProductList).ToList();
        }
        #endregion

        #region UPDATE
        // Update a product
        public async Task<Product> UpdateProduct(int id, UpdateProductDTO dto)
        {
            var product = await GetProduct(id);
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
            var product = await GetProduct(id);
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
