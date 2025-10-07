using System.Security.Claims;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AubgEMS.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _events;
        private readonly ILookupService _lookups;
        private readonly IAttendanceService _attendance;

        public EventsController(IEventService events, ILookupService lookups, IAttendanceService attendance)
        {
            _events = events;
            _lookups = lookups;
            _attendance = attendance;
        }

        // GET /Events
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] EventQuery query, CancellationToken ct)
        {
            // Create a normalized copy without losing filters (init-only props safe via initializer)
            var normalized = new EventQuery(
                page:    query.Page     < 1 ? 1  : query.Page,
                pageSize:query.PageSize < 1 ? 10 : query.PageSize)
            {
                EventTypeId  = query.EventTypeId,
                DepartmentId = query.DepartmentId,
                ClubId       = query.ClubId,
                Search       = query.Search
            };

            var result = await _events.GetAllAsync(normalized, ct);

            // Lookups for filters (preselect current query values)
            var eventTypes  = await _lookups.EventTypesAsync(ct);
            var departments = await _lookups.DepartmentsAsync(ct);
            var clubs       = await _lookups.ClubsAsync(normalized.DepartmentId, ct);

            ViewBag.EventTypes  = new SelectList(eventTypes,  "Id", "Name", normalized.EventTypeId);
            ViewBag.Departments = new SelectList(departments, "Id", "Name", normalized.DepartmentId);
            ViewBag.Clubs       = new SelectList(clubs,       "Id", "Name", normalized.ClubId);
            ViewBag.Query       = normalized; // keep current filters/search in the view

            return View(result);
        }

        // GET /Events/Details/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var model = await _events.GetByIdAsync(id, ct);
            if (model is null) return NotFound();

            // For signed-in users, tell the view whether they already joined
            bool joinedByMe = false;
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                joinedByMe = await _attendance.IsJoinedAsync(id, userId, ct);
            }
            ViewBag.JoinedByMe = joinedByMe;

            return View(model);
        }

        // POST /Events/Join
        [HttpPost]
        [Authorize(Policy = "RequireSignedIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int id, string? returnUrl = null, CancellationToken ct = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _attendance.JoinAsync(id, userId, ct);
            return SafeBack(returnUrl, id);
        }

        // POST /Events/Leave
        [HttpPost]
        [Authorize(Policy = "RequireSignedIn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(int id, string? returnUrl = null, CancellationToken ct = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _attendance.LeaveAsync(id, userId, ct);
            return SafeBack(returnUrl, id);
        }

        private IActionResult SafeBack(string? returnUrl, int eventId)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Details), new { id = eventId });
        }
    }
}