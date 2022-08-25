using Microsoft.Extensions.DependencyInjection;
using RushRoyale.Application.Features.News;
using RushRoyale.Application.Features.Player.Profile;

namespace RushRoyale.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<NewsService>();
        services.AddSingleton<ProfileService>();

        return services;
    }
}