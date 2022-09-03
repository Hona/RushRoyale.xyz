using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Services;
using RushRoyale.Discord.Services;
using RushRoyale.WebAPI.Utilities;
using RushRoyale.WebAPI.ViewModels;

namespace RushRoyale.WebAPI.Controllers.Player;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DiscordController : Controller
{
    private readonly CurrentUserService _currentUserService;
    private readonly DiscordService _discordService;

    public DiscordController(DiscordService discordService, CurrentUserService currentUserService)
    {
        _discordService = discordService;
        _currentUserService = currentUserService;
    }

    [HttpGet("guilds")]
    [ProducesResponseType(typeof(List<UserGuild>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuilds()
    {
        var userId = _currentUserService.GetUserId();
        var discordId = DiscordService.GetDiscordId(userId);
        
        var guilds = await _discordService.GetUserGuildsAsync(discordId);
            
        var output = guilds.Select(x => x.ToUserGuild());

        return Ok(output);
    }
    
    
    [HttpGet("guilds/{id}/roles")]
    [ProducesResponseType(typeof(List<GuildRole>), StatusCodes.Status200OK)]
    public IActionResult GetGuildRoles(ulong id)
    {
        var roles = _discordService.GetRoles(id)
            .Select(x => new GuildRole
            {
                Id = x.Id,
                Name = x.Name,
                Users = x.Members
                    .Select(member => member.ToGuildUser())
                    .ToList()
            });

        return Ok(roles);
    }
    
    [HttpGet("guilds/{guildId}/roles/{roleId}/members")]
    [ProducesResponseType(typeof(List<GuildUser>), StatusCodes.Status200OK)]
    public IActionResult GetGuildRoleMembers(ulong guildId, ulong roleId)
    {
        var roleMembers = _discordService.GetRoleMembers(guildId, roleId)
            .Select(x => x.ToGuildUser());

        return Ok(roleMembers);
    }
}