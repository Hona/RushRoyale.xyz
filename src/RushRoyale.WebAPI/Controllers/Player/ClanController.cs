using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Features.Player.Clans;
using RushRoyale.Application.Features.Player.Clans.Models;

namespace RushRoyale.WebAPI.Controllers.Player;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClanController : Controller
{
    private readonly CurrentUserService _currentUserService;
    private readonly ClanService _clanService;

    public ClanController(ClanService clanService, CurrentUserService currentUserService)
    {
        _clanService = clanService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RegisterClan([FromBody] RegisteredClan clan)
    {
        var userId = _currentUserService.GetUserId();

        clan.UserId = userId;
        await _clanService.RegisterClanAsync(clan);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<RegisteredClan>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClans()
    {
        var userId = _currentUserService.GetUserId();

        var output = await _clanService.GetRegisteredClansAsync(userId);

        return Ok(output);
    }
}