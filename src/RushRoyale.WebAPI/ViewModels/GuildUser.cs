namespace RushRoyale.WebAPI.ViewModels;

public class GuildUser
{
    public ulong Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string IconUri { get; set; } = string.Empty;
}