using ProductCatalog.Api.Data.Interface;
using ProductCatalog.Api.Data.Repo;

namespace ProductCatalog.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
        }
    }
}
