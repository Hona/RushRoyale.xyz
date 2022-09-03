using Discord.WebSocket;
using RushRoyale.Application.Features.Player.Clans.Models;
using RushRoyale.WebAPI.ViewModels;

namespace RushRoyale.WebAPI.Utilities;

public static class EntityMappingExtensions
{
    public static Clan ToPartialClan(this RegisteredClan clan) => new()
    {
        Id = clan.Id,
        DisplayName = clan.DisplayName,
        GuildId = clan.GuildId,
        RoleId = clan.RoleId,
        SandalsWarningMessage = clan.SandalsWarningMessage
    };

    public static GuildUser ToGuildUser(this SocketGuildUser user) => new()
    {
        Id = user.Id,
        DisplayName = user.DisplayName ?? user.Nickname ?? user.Username,
        IconUri = user.GetGuildAvatarUrl() ?? user.GetDisplayAvatarUrl() ?? user.GetDefaultAvatarUrl()
    };
    
    public static UserGuild ToUserGuild(this SocketGuild guild) => new()
    {
        Id = guild.Id,
        Name = guild.Name,
        IconUrl = guild.IconUrl
    };
}