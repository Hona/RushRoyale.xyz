using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RushRoyale.Discord.Services;

namespace RushRoyale.Discord;

public static class DependencyInjection
{
    public static IServiceCollection AddDiscord(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All,
            AlwaysDownloadUsers = true
        });
        
        var discordBotConfig = new RushRoyaleDiscordBotConfig();
        config.Bind("DiscordBot", discordBotConfig);
        services.AddSingleton(discordBotConfig);

        services.AddHostedService<DiscordBotHostedService>();

        services.AddSingleton<DiscordService>();
        
        return services;
    }
}