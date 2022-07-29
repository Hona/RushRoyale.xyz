using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RushRoyale.WebApiClient;
using RushRoyale.WebUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

var apiBaseUrl = builder.Configuration["Api:BaseUrl"];
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl ?? throw new InvalidOperationException()) });

builder.Services.AddScoped(x => new AuthenticationClient("", x.GetRequiredService<HttpClient>()));

builder.Services.AddOidcAuthentication(x =>
{
    builder.Configuration.Bind("Discord", x.ProviderOptions);
});

await builder.Build().RunAsync();