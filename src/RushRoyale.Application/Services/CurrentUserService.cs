using System.Security.Claims;
using Discord.WebSocket;
using Microsoft.AspNetCore.Http;
using RushRoyale.Discord.Services;

namespace RushRoyale.Application.Services;

public class CurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DiscordService _discordService;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, DiscordService discordService)
    {
        _httpContextAccessor = httpContextAccessor;
        _discordService = discordService;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException();
    }

    public SocketUser GetDiscordUser()
    {
        var userId = GetUserId();
        var discordUserId = ulong.Parse(userId.Split("|").Last());
        var user = _discordService.GetUser(discordUserId);
        return user ?? throw new InvalidOperationException();
    }
}