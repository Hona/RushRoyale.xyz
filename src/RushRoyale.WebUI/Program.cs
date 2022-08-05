using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RushRoyale.WebApiClient;
using RushRoyale.WebUI;
using RushRoyale.WebUI.Models;
using RushRoyale.WebUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient<DataService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration["UI:BaseUrl"], UriKind.Absolute);
});

builder.Services.Configure<Developer>(builder.Configuration.GetSection(nameof(Developer)));

builder.Services.AddSingleton<DataServiceCache>();
builder.Services.AddSingleton<ToolsService>();

var apiBaseUrl = builder.Configuration["Api:BaseUrl"];
builder.Services.AddHttpClient<AuthenticationClient>(x =>
{
    x.BaseAddress = new Uri(apiBaseUrl ?? throw new InvalidOperationException());
});
builder.Services.AddHttpClient();
await builder.Build().RunAsync();