using System.Net.Http.Json;
using RushRoyale.WebUI.Models;
using RushRoyale.WebUI.Models.DataMine;
using RushRoyale.WebUI.Models.DeckSuggestions;

namespace RushRoyale.WebUI.Services;

public class DataService
{
    private readonly HttpClient _httpClient;

    private readonly DataServiceCache _cache;
    
    public DataService(HttpClient httpClient, DataServiceCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    private async Task AddToCache<TModel>(string endpoint, CancellationToken cancellationToken = default) where TModel : class
    {
        var data = await _httpClient.GetFromJsonAsync<TModel>(endpoint, cancellationToken);
        
        _cache.MemoryCache[typeof(TModel)] = data;
    }

    private async Task<IReadOnlyList<T>?> GetListAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : class
        => await GetAsync<IReadOnlyList<T>>(endpoint, cancellationToken);
    
    private async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : class
    {
        if (_cache.IsInCache<T>())
        {
            return _cache.GetFromCache<T>();
        }
        
        await AddToCache<T>(endpoint, cancellationToken);
        return _cache.GetFromCache<T>();
    }

    public async Task<IReadOnlyList<Unit>?> GetUnitsAsync(CancellationToken cancellationToken = default)
        => await GetListAsync<Unit>("data/units.json", cancellationToken);

    public async Task<IReadOnlyList<Faction>?> GetFactionsAsync(CancellationToken cancellationToken = default)
        => await GetListAsync<Faction>("data/factions.json", cancellationToken);
    
    public async Task<LocalizationDictionary?> GetLocalizationsAsync(CancellationToken cancellationToken = default)
        => await GetAsync<LocalizationDictionary>("data-mine/localizations.json", cancellationToken);
    
    public async Task<IReadOnlyList<DeckSuggestion>?> GetDeckSuggestionsAsync(CancellationToken cancellationToken = default)
        => await GetListAsync<DeckSuggestion>("data/deck-suggestions.json", cancellationToken);
}