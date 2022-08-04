using System.Net.Http.Json;
using RushRoyale.WebUI.Models;

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

    private async Task AddToCache<TModel>(string endpoint, CancellationToken cancellationToken = default)
    {
        var data = await _httpClient.GetFromJsonAsync<IReadOnlyList<TModel>>(endpoint, cancellationToken);
        
        _cache.MemoryCache[typeof(TModel)] = data as IReadOnlyList<object>;
    }

    private async Task<IReadOnlyList<T>?> Get<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        if (_cache.IsInCache<T>())
        {
            return _cache.GetFromCache<T>();
        }
        
        await AddToCache<T>(endpoint, cancellationToken);
        return _cache.GetFromCache<T>();
    }

    public async Task<IReadOnlyList<Unit>?> GetUnitsAsync(CancellationToken cancellationToken = default)
        => await Get<Unit>("data/units.json", cancellationToken);

    public async Task<IReadOnlyList<Faction>?> GetFactionsAsync(CancellationToken cancellationToken = default)
        => await Get<Faction>("data/factions.json", cancellationToken);

}