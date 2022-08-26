using System.Collections.ObjectModel;
using Discord;
using Discord.WebSocket;

namespace RushRoyale.Discord.Services;

public class DiscordService
{
    private readonly DiscordSocketClient _discordSocketClient;

    public DiscordService(DiscordSocketClient discordSocketClient)
    {
        _discordSocketClient = discordSocketClient;
    }

    private void EnsureClientConnected()
    {
        
        return;
        
        if (_discordSocketClient.ConnectionState is not ConnectionState.Connected)
        {
            throw new ApplicationException("Discord client is not connected.");
        }
    }

    public static ulong GetDiscordId(string auth0Id)
    {
        const string auth0Prefix = "oauth2|discord|";
        if (!auth0Id.StartsWith(auth0Prefix, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException("Current user is not a Discord user.");
        }
        
        return ulong.Parse(auth0Id[auth0Prefix.Length..]);
    }

    public async Task<IReadOnlyList<SocketGuild>> GetUserGuildsAsync(ulong userId)
    {
        EnsureClientConnected();

        var output = new List<SocketGuild>();
        foreach (var guild in _discordSocketClient.Guilds)
        {
            await guild.DownloadUsersAsync();

            if (guild.Users.Any(user => user.Id == userId))
            {
                output.Add(guild);
            }
        }
        
        return output.AsReadOnly();
    }
    
    public IReadOnlyList<SocketRole> GetRoles(ulong guildId, bool excludeEveryone = true)
    {
        EnsureClientConnected();
        
        var guild = _discordSocketClient.GetGuild(guildId);
        
        var output = guild.Roles;

        if (excludeEveryone)
        {
            output = output.Where(role => role.Id != guild.EveryoneRole.Id).ToList();
        }
            
        return output.OrderByDescending(x => x.Position)
            .ToList()
            .AsReadOnly();
    }
}