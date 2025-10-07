using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Clubs;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AubgEMS.Controllers
{
    public class ClubsController : Controller
    {
        private readonly IClubService _clubs;
        private readonly IEventService _events;
        private readonly ILookupService _lookups;

        public ClubsController(IClubService clubs, IEventService events, ILookupService lookups)
        {
            _clubs = clubs;
            _events = events;
            _lookups = lookups;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int? departmentId,
            string? search,
            int page = 1,
            int pageSize = 10,
            CancellationToken ct = default)
        {
            var depts = await _lookups.DepartmentsAsync(ct);
            ViewBag.Departments = new SelectList(depts, "Id", "Name", departmentId);

            var query = new ClubQuery(page, pageSize)
            {
                DepartmentId = departmentId,
                Search = string.IsNullOrWhiteSpace(search) ? null : search.Trim()
            };

            var result = await _clubs.GetAllAsync(query, ct);

            ViewBag.SelectedDepartmentId = departmentId;
            ViewBag.Search = search;
            ViewBag.PageSize = pageSize;

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            var club = await _clubs.GetDetailsAsync(id, ct);
            if (club is null) return NotFound();

            var eventsPage = await _events.GetAllAsync(new EventQuery(page, pageSize)
            {
                ClubId = id
            }, ct);

            var vm = new ClubDetailsPageVm
            {
                Club = club,
                Events = eventsPage
            };

            return View(vm);
        }
    }
}
