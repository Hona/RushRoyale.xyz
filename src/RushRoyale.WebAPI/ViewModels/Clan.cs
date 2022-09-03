namespace RushRoyale.WebAPI.ViewModels;

public class Clan
{
    public string Id { get; set; } = string.Empty;
    public ulong GuildId { get; set; }
    public ulong RoleId { get; set; }
    public string DisplayName { get; set; } = string.Empty;

    public List<GuildUser> WhitelistedUsers { get; set; } = new();
    public List<GuildUser> BlacklistedUsers { get; set; } = new();
    public List<GuildUser> RoleUsers { get; set; } = new();
    
    
}