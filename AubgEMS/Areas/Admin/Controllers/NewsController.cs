using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class NewsController : Controller
    {
        private readonly INewsService _news;
        public NewsController(INewsService news) => _news = news;

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, CancellationToken ct = default)
        {
            var result = await _news.GetAllAsync(new PageQuery(page, 10), ct);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create() => View(new NewsEditDto());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsEditDto model, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return View(model);

            await _news.CreateAsync(model, ct);
            TempData["StatusMessage"] = "News created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct = default)
        {
            var n = await _news.GetDetailsAsync(id, ct);
            if (n is null) return NotFound();

            var vm = new NewsEditDto { Title = n.Title, Body = n.Body, ImageUrl = n.ImageUrl };
            ViewBag.NewsId = id;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsEditDto model, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NewsId = id;
                return View(model);
            }

            var ok = await _news.UpdateAsync(id, model, ct);
            if (!ok) return NotFound();

            TempData["StatusMessage"] = "News updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/News/Delete/5  (confirmation)
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var dto = await _news.GetDetailsAsync(id, ct);
            if (dto is null) return NotFound();
            return View(dto);
        }

        // POST: /Admin/News/Delete  (actual delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct = default)
        {
            var ok = await _news.DeleteAsync(id, ct);
            TempData["StatusMessage"] = ok ? "News deleted." : "News not found.";
            return RedirectToAction(nameof(Index));
        }
    }
}