using System.Security.Claims;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Features.Identity;

namespace RushRoyale.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtService _jwtService;
    
    public AuthenticationController(ILogger<AuthenticationController> logger, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _jwtService = jwtService;
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? returnUrl)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var identity = httpContext?.User.Identity;

        // Check if user is authenticated
        if (identity is { IsAuthenticated: true })
        {
            return Redirect(returnUrl ?? "/");
        }
        
        // Challenge user to login with Discord
        return Challenge(DiscordAuthenticationDefaults.AuthenticationScheme);
    }
    
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var identity = httpContext?.User.Identity;
        
        // Check if user is authenticated
        if (identity is { IsAuthenticated: true })
        {
            return Ok(identity.Name);
        }
        
        return Unauthorized();
    }
}