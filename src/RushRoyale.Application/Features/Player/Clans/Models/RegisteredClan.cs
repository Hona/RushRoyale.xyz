using RushRoyale.Infrastructure;

namespace RushRoyale.Application.Features.Player.Clans.Models;

public class RegisteredClan : ICosmosDocument
{
    public string Id => UserId + GuildId +  RoleId;
    public string? UserId { get; set; } = string.Empty;
    public ulong GuildId { get; set; }
    public ulong RoleId { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public List<ulong> WhitelistedUsers { get; set; } = new();
    public List<ulong> BlacklistedUsers { get; set; } = new();
}