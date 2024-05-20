using ProductCatalog.Shared.DTOs;
using System.Net.Http.Json;

namespace YourBlazorApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region CATEGORIES
        // Categories
        public async Task<List<CategoryDTO>> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("api/category");
        }

        public async Task<CategoryContentDTO> GetCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryContentDTO>($"api/category/{id}");
        }

        public async Task<HttpResponseMessage> CreateCategory(CreateCategoryDTO category)
        {
            return await _httpClient.PostAsJsonAsync("api/category/create", category);
        }

        public async Task<HttpResponseMessage> UpdateCategory(int id, CreateCategoryDTO category)
        {
            return await _httpClient.PutAsJsonAsync($"api/category/update/{id}", category);
        }

        public async Task<HttpResponseMessage> DeleteCategory(int id)
        {
            return await _httpClient.DeleteAsync($"api/category/delete/{id}");
        }
        #endregion

        #region PRODUCTS
        // Products
        public async Task<List<ProductDTO>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDTO>>("api/product");
        }

        public async Task<ProductDetailsDTO> GetProduct(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDetailsDTO>($"api/product/{id}");
        }

        public async Task<HttpResponseMessage> CreateProduct(CreateProductDTO product)
        {
            return await _httpClient.PostAsJsonAsync("api/product/create", product);
        }

        public async Task<HttpResponseMessage> UpdateProduct(int id, UpdateProductDTO product)
        {
            return await _httpClient.PutAsJsonAsync($"api/product/update/{id}", product);
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            return await _httpClient.DeleteAsync($"api/product/delete/{id}");
        }

        public async Task<List<ProductDTO>> GetProductByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDTO>>($"api/product/search/{name}");
        }
        #endregion
    }
}
