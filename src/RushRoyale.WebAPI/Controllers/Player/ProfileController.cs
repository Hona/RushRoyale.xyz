using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Features.Player.Profile;
using RushRoyale.Application.Features.Player.Profile.Models;
using RushRoyale.Application.Services;

namespace RushRoyale.WebAPI.Controllers.Player;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : Controller
{
    private readonly ProfileService _profileService;
    private readonly CurrentUserService _currentUserService;
    
    public ProfileController(ProfileService profileService, CurrentUserService currentUserService)
    {
        _profileService = profileService;
        _currentUserService = currentUserService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PlayerProfile), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile()
    {
        var userId = _currentUserService.GetUserId();
        var profile = await _profileService.GetProfileAsync(userId);

        profile ??= new PlayerProfile();
        
        return Ok(profile);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateOrUpdateProfile([FromBody] PlayerProfile profile)
    {
        var userId = _currentUserService.GetUserId();
        profile.UserId = userId;        
        
        await _profileService.CreateOrUpdateProfileAsync(profile);
        
        return NoContent();
    }
}