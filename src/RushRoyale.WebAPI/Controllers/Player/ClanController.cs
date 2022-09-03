using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Features.Player.Clans;
using RushRoyale.Application.Features.Player.Clans.Models;
using RushRoyale.Application.Services;
using RushRoyale.Discord.Services;
using RushRoyale.WebAPI.Utilities;
using RushRoyale.WebAPI.ViewModels;

namespace RushRoyale.WebAPI.Controllers.Player;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClanController : Controller
{
    private readonly CurrentUserService _currentUserService;
    private readonly ClanService _clanService;
    private readonly DiscordService _discordService;

    public ClanController(ClanService clanService, CurrentUserService currentUserService, DiscordService discordService)
    {
        _clanService = clanService;
        _currentUserService = currentUserService;
        _discordService = discordService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RegisterClan([FromBody] RegisteredClan clan)
    {
        var userId = _currentUserService.GetUserId();

        clan.UserId = userId;
        await _clanService.StoreClanAsync(clan);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Clan>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClans()
    {
        var userId = _currentUserService.GetUserId();

        var clans = await _clanService.GetRegisteredClansAsync(userId);

        var output = clans.Select(x => x.ToPartialClan());
        
        return Ok(output);
    }
    
    [HttpGet("guilds/{guildId}/roles/{roleId}")]
    [ProducesResponseType(typeof(Clan), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClan(ulong guildId, ulong roleId)
    {
        var userId = _currentUserService.GetUserId();

        var clan = await _clanService.GetClan(userId, guildId, roleId);

        var output = clan.ToPartialClan();

        output.RoleUsers = _discordService.GetRoleMembers(guildId, roleId)
            .Select(x => x.ToGuildUser())
            .ToList();

        output.WhitelistedUsers = _discordService
            .LookupGuildUsers(guildId, clan.WhitelistedUsers)
            .Select(x => x.ToGuildUser())
            .ToList();

        output.BlacklistedUsers = _discordService
            .LookupGuildUsers(guildId, clan.BlacklistedUsers)
            .Select(x => x.ToGuildUser())
            .ToList();
        
        return Ok(output);
    }
    
    [HttpPut("guilds/{guildId}/roles/{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateClan(ulong guildId, ulong roleId, [FromBody] Clan clan)
    {
        var userId = _currentUserService.GetUserId();

        await _clanService.StoreClanAsync(new RegisteredClan
        {
            DisplayName = clan.DisplayName,
            GuildId = guildId,
            RoleId = roleId,
            UserId = userId,
            SandalsWarningMessage = clan.SandalsWarningMessage,
            BlacklistedUsers = clan.BlacklistedUsers.Select(x => x.Id).ToList(),
            WhitelistedUsers = clan.WhitelistedUsers.Select(x => x.Id).ToList()
        });

        return NoContent();
    }
    
    [HttpDelete("guilds/{guildId}/roles/{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteClan(ulong guildId, ulong roleId)
    {
        var userId = _currentUserService.GetUserId();

        await _clanService.DeleteClanAsync(userId, guildId, roleId);

        return NoContent();
    }

    [HttpPost("guilds/{guildId}/roles/{roleId}/users/{userId}/warn")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> WarnUser(ulong guildId, ulong roleId, ulong userId)
    {
        var currentUserId = _currentUserService.GetUserId();

        await _clanService.WarnUserAsync(currentUserId, guildId, roleId, userId);

        return NoContent();
    }
}