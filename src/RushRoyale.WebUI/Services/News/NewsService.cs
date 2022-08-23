using System.Net.Http.Json;
using RushRoyale.WebUI.Services.News.Models;

namespace RushRoyale.WebUI.Services;

public class NewsService
{
    private readonly HttpClient _httpClient = new();

    private static readonly Uri NewsBaseUri = new("https://static.rush.my.games/News/", UriKind.Absolute);
    
    public async Task<NewsIndex?> GetNewsIndexAsync()
    {
        var newsIndexUri = new Uri(NewsBaseUri, "newsindex.json?t=" + DateTime.UtcNow.Ticks);
        
        return await _httpClient.GetFromJsonAsync<NewsIndex>(newsIndexUri);
    }
    
    public async Task<NewsArticle?> GetNewsArticleAsync(string fileName)
    {
        var newsArticleUri = new Uri(NewsBaseUri, fileName);
        
        return await _httpClient.GetFromJsonAsync<NewsArticle>(newsArticleUri);
    }
}