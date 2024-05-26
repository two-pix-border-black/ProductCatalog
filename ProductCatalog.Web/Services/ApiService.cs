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
        #endregion

        #region PRODUCTS
        // Products
        public async Task<List<ProductDetailsDTO>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDetailsDTO>>("api/product");
        }

        public async Task<ProductDetailsDTO> GetProduct(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDetailsDTO>($"api/product/{id}");
        }

        public async Task<List<ProductDTO>> GetProductByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDTO>>($"api/product/search/{name}");
        }

        public async Task<List<string>> GetUniqueColors()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/product/colors");
        }
        #endregion
    }
}
