using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly INewsService _news;

        public NewsController(INewsService news) => _news = news;

        // /News?page=1
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;
            var result = await _news.GetAllAsync(new PageQuery(page, pageSize));
            return View(result);
        }

        // /News/Details/3
        public async Task<IActionResult> Details(int id)
        {
            var model = await _news.GetDetailsAsync(id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var model = await _news.GetDetailsAsync(id);
            if (model is null) return NotFound();

            // Get a pool and pick up to 9 random items (excluding the current one)
            var page = await _news.GetAllAsync(new PageQuery(page: 1, pageSize: 18), ct);
            var more = (page.Items ?? Enumerable.Empty<NewsListItemDto>())
                .Where(x => x.Id != id)
                .OrderBy(_ => Guid.NewGuid())
                .Take(9)
                .ToList();

            ViewBag.MoreNews = more;
            return View(model);
        }
    }
}