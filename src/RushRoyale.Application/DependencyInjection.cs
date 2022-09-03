using Microsoft.Extensions.DependencyInjection;
using RushRoyale.Application.Features.News;
using RushRoyale.Application.Features.Player.Clans;
using RushRoyale.Application.Features.Player.Profile;

namespace RushRoyale.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<NewsService>();
        services.AddSingleton<ProfileService>();
        services.AddScoped<ClanService>();

        return services;
    }
}