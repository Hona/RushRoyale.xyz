using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RushRoyale.Discord.Services;

public class DiscordBotHostedService : IHostedService
{
    private readonly DiscordSocketClient _discordSocketClient;
    private readonly RushRoyaleDiscordBotConfig _config;

    public DiscordBotHostedService(DiscordSocketClient discordSocketClient, RushRoyaleDiscordBotConfig config, ILogger<DiscordSocketClient> discordLogger)
    {
        _discordSocketClient = discordSocketClient;
        _config = config;

        _discordSocketClient.Log += (message =>
        {
            discordLogger.Log(message.Severity.ToLogLevel(), message.Exception, message.Message);
            
            return Task.CompletedTask;
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _discordSocketClient.LoginAsync(TokenType.Bot, _config.Token);
        await _discordSocketClient.StartAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _discordSocketClient.LogoutAsync();
    }
}