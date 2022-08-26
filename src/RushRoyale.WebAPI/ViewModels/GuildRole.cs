namespace RushRoyale.WebAPI.ViewModels;

public class GuildRole
{
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<GuildUser> Users { get; set; } = new();
}