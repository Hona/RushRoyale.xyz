using Microsoft.AspNetCore.Mvc;
using RushRoyale.Application.Features.News;
using RushRoyale.Application.Features.News.Models;

namespace RushRoyale.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : Controller
{
    private readonly NewsService _newsService;

    public NewsController(NewsService newsService)
    {
        _newsService = newsService;
    }

    [ProducesResponseType(typeof(NewsIndex), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetIndexAsync()
    {
        var output = await _newsService.GetNewsIndexAsync();

        output ??= new NewsIndex();
        
        return Ok(output);
    }
    
    [ProducesResponseType(typeof(NewsArticle), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetArticleAsync(string fileName)
    {
        var output = await _newsService.GetNewsArticleAsync(fileName);

        if (output is null)
        {
            return NotFound();
        }
        
        return Ok(output);
    }
}