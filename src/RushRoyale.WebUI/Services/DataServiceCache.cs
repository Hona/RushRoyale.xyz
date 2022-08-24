namespace RushRoyale.WebUI.Services;

public class DataServiceCache
{
    public Dictionary<Type, object?> MemoryCache { get; } = new();
    
    public T? GetFromCache<T>() where T : class => MemoryCache[typeof(T)] as T;

    public bool IsInCache<T>() => MemoryCache.ContainsKey(typeof(T));
}