namespace RushRoyale.WebUI.Services;

public class LocalizationService
{
    private readonly DataService _dataService;

    public LocalizationService(DataService dataService)
    {
        _dataService = dataService;
    }
    
    public async Task<string> GetLocalizedValue(string key)
    {
        var localization = await _dataService.GetLocalizationsAsync();
        
        ArgumentNullException.ThrowIfNull(localization);
        
        return localization[key];
    }
}