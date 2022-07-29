using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace RushRoyale.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthenticationController(ILogger<AuthenticationController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    [ProducesResponseType(StatusCodes.Status302Found)]
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string returnUrl = "/")
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        // Check if user is authenticated
        if (httpContext?.User.Identity?.IsAuthenticated is true)
        {
            _logger.LogInformation("User is already authenticated");
            return Redirect(returnUrl);
        }
        
        // Challenge user to login with Discord
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = returnUrl
        }, DiscordAuthenticationDefaults.AuthenticationScheme);
    }
}