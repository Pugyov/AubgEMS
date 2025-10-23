using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AubgEMS.Models;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;

namespace AubgEMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewsService _news;

    public HomeController(ILogger<HomeController> logger, INewsService news)
    {
        _logger = logger;
        _news = news;
    }

    // GET /
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        // Fetch the latest 3 news via paging
        var page = await _news.GetAllAsync(new PageQuery(page: 1, pageSize: 3), ct);
        var latest = page.Items; // IEnumerable<NewsListItemDto>
        return View(latest);     // Views/Home/Index.cshtml
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AllowAnonymous]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Terms() => View();
}