using System;
using System.Threading;
using System.Threading.Tasks;
using AubgEMS.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class DashboardController : Controller
    {
        private readonly IAnalyticsService _analytics;
        public DashboardController(IAnalyticsService analytics) => _analytics = analytics;

        // GET: /Admin/Dashboard
        [HttpGet]
        public async Task<IActionResult> Index(
            DateTime? from = null,
            DateTime? to = null,
            int? departmentId = null,
            int? clubId = null,
            int? eventTypeId = null,
            CancellationToken ct = default)
        {
            // Default range: last 30 days (UTC)
            var toUtc   = (to   ?? DateTime.UtcNow).Date.AddDays(1).AddTicks(-1); // end of day
            var fromUtc = (from ?? DateTime.UtcNow.AddDays(-30)).Date;

            var kpis = await _analytics.GetKpisAsync(fromUtc, toUtc, departmentId, clubId, eventTypeId, ct);

            ViewBag.From = fromUtc;
            ViewBag.To   = toUtc;
            return View(kpis);
        }
    }
}