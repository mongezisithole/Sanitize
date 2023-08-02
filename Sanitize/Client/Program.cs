using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sanitize.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Not a good thing to hard code Url here but for demo purposes I think it is enough
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7049") });

await builder.Build().RunAsync();
