using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RushRoyale.WebApiClient;
using RushRoyale.WebUI;
using RushRoyale.WebUI.Models;
using RushRoyale.WebUI.Models.DataMine;
using RushRoyale.WebUI.Pages.DataMine;
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
builder.Services.AddSingleton<LocalizationService>();

var apiBaseUrl = builder.Configuration["Api:BaseUrl"];

const string ApiClient = nameof(ApiClient);
builder.Services.AddHttpClient(ApiClient, 
    client => client.BaseAddress = new Uri(apiBaseUrl ?? throw new InvalidOperationException()));

builder.Services.AddHttpClient<NewsClient>(ApiClient);

builder.Services.AddHttpClient();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();