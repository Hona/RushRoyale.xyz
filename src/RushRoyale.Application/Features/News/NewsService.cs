using System.Net.Http.Json;
using RushRoyale.Application.Features.News.Models;

namespace RushRoyale.Application.Features.News;

public class NewsService
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://static.rush.my.games/News/", UriKind.Absolute)
    };

    public async Task<NewsIndex?> GetNewsIndexAsync()
    {
        var newsIndexUri = "newsindex.json?t=" + DateTime.UtcNow.Ticks;

        return await _httpClient.GetFromJsonAsync<NewsIndex>(newsIndexUri);
    }
    
    public async Task<NewsArticle?> GetNewsArticleAsync(string fileName) 
        => await _httpClient.GetFromJsonAsync<NewsArticle>(fileName);
}