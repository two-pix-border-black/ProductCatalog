using ProductCatalog.Api.Models;
using ProductCatalog.Api.Models.DTOs;

namespace ProductCatalog.Api.Data.Interface
{
    public interface IProductRepo
    {
        public Task<Product> CreateProduct(CreateProductDTO dto);
        public Task<bool> DeleteProduct(int id);
        public Task<ProductDetailsDTO> GetProduct(int id);
        public Task<List<ProductDTO>> GetProductByName(string name);
        public Task<List<ProductDTO>> GetProducts();
        public Task<Product> UpdateProduct(int id, UpdateProductDTO dto);
    }
}
