using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace RushRoyale.WebApiClient;

public static class DependencyInjection
{
    public static IServiceCollection AddApiClients(this IServiceCollection services, string apiBaseUrl)
    {
        const string ApiClient = nameof(ApiClient);
        services.AddHttpClient(ApiClient, 
                client => client.BaseAddress = new Uri(apiBaseUrl ?? throw new InvalidOperationException()))
            .AddHttpMessageHandler(sp =>
                sp.GetRequiredService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { apiBaseUrl }
                    ));

        services.AddHttpClient<NewsClient>(ApiClient);
        services.AddHttpClient<ProfileClient>(ApiClient);
        services.AddHttpClient<DiscordClient>(ApiClient);
        services.AddHttpClient<ClanClient>(ApiClient);
        
        return services;
    }
}