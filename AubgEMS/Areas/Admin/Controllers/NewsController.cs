using AubgEMS.Areas.Admin.Controllers;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Areas.Admin.Controllers
{
    // If you created AdminBaseController earlier, inherit from it.
    // Otherwise keep the attributes here.
    [Area("Admin")]
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "RequireAdmin")]
    public class NewsController : Controller // or : AdminBaseController
    {
        private readonly INewsService _news;

        public NewsController(INewsService news) => _news = news;

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _news.GetAllAsync(new PageQuery(page, 10));
            return View(result);
        }

        public IActionResult Create() => View(new NewsEditDto());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsEditDto model)
        {
            if (!ModelState.IsValid) return View(model);

            await _news.CreateAsync(model);
            TempData["StatusMessage"] = "News created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var n = await _news.GetDetailsAsync(id);
            if (n is null) return NotFound();

            var vm = new NewsEditDto
            {
                Title = n.Title,
                Body = n.Body,
                ImageUrl = n.ImageUrl
            };
            ViewBag.NewsId = id;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsEditDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NewsId = id;
                return View(model);
            }

            var ok = await _news.UpdateAsync(id, model);
            if (!ok) return NotFound();

            TempData["StatusMessage"] = "News updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _news.DeleteAsync(id);
            TempData["StatusMessage"] = ok ? "News deleted." : "News not found.";
            return RedirectToAction(nameof(Index));
        }
    }
}
