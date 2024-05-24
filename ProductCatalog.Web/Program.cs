using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductCatalog.Web;
using YourBlazorApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://maneroproductsapi.azurewebsites.net/") });

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();
