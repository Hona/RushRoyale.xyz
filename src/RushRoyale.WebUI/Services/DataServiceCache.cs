namespace RushRoyale.WebUI.Services;

public class DataServiceCache
{
    public Dictionary<Type, IReadOnlyList<object>?> MemoryCache { get; } = new();
    
    public IReadOnlyList<T>? GetFromCache<T>() => MemoryCache[typeof(T)] as IReadOnlyList<T>;

    public bool IsInCache<T>() => MemoryCache.ContainsKey(typeof(T));
}