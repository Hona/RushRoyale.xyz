using Microsoft.Extensions.Options;
using RushRoyale.WebUI.Models;

namespace RushRoyale.WebUI.Services;

public class ToolsService
{
    private readonly IOptions<Developer> _developerOptions;
    private bool? _developerOverride;

    public event EventHandler? OnToolsUpdated;

    public ToolsService(IOptions<Developer> developerOptions)
    {
        _developerOptions = developerOptions;
    }

    public bool? DeveloperOverride
    {
        get => _developerOverride;
        set
        {
            _developerOverride = value;
            OnToolsUpdated?.Invoke(null, EventArgs.Empty);
        }
    }

    public bool IsDeveloper => DeveloperOverride ?? _developerOptions.Value.Enabled;
}