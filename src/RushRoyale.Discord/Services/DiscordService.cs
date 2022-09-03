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

    public IReadOnlyList<SocketGuildUser> GetRoleMembers(ulong guildId, ulong roleId)
    {
        var guild = _discordSocketClient.GetGuild(guildId);

        var role = guild.Roles.SingleOrDefault(x => x.Id == roleId);

        ArgumentNullException.ThrowIfNull(role);
        
        return role.Members
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<SocketGuildUser> LookupGuildUsers(ulong guildId, IEnumerable<ulong> userIds)
    {
        var guild = _discordSocketClient.GetGuild(guildId);
        
        return guild.Users
            .Where(x => userIds.Contains(x.Id))
            .ToList()
            .AsReadOnly();
    }

    public SocketUser GetUser(ulong userId) => _discordSocketClient.GetUser(userId);

    public async Task<IUserMessage> MessageUserAsync(ulong userId, string? message = null, Embed? embed = null)
    {
        var user = _discordSocketClient.GetUser(userId);
        return await user.SendMessageAsync(message, embed: embed);
    }
}