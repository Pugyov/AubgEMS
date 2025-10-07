using AubgEMS.Core.Constants;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AubgEMS.Areas.Organizer.Controllers
{
    [Area("Organizer")]
    [Authorize(Policy = "RequireOrganizerOrAdmin")]
    public class EventsController : OrganizerBaseController
    {
        private readonly IEventService _events;
        private readonly ILookupService _lookups;

        public EventsController(IEventService events, ILookupService lookups)
        {
            _events = events;
            _lookups = lookups;
        }

        // GET: /Organizer/Events?all=false&page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 10,
            bool all = false,
            CancellationToken ct = default)
        {
            // Admin can pass all=true to see all events; organizers always see their own.
            var showAll = all && User.IsInRole(RoleNames.Admin);
            ViewBag.Scope = showAll ? "all" : "mine";

            if (showAll)
            {
                var result = await _events.GetAllAsync(new EventQuery(page, pageSize), ct);
                return View(result);
            }
            else
            {
                var result = await _events.GetCreatedByAsync(CurrentUserId, new PageQuery(page, pageSize), ct);
                return View(result);
            }
        }

        // GET: /Organizer/Events/Create
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            await LoadLookupsAsync(ct);
            return View(new EventCreateDto { StartTime = DateTime.UtcNow.AddDays(1) });
        }

        // POST: /Organizer/Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateDto model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync(ct);
                return View(model);
            }

            var created = await _events.CreateAsync(model, CurrentUserId, ct);
            if (!created.Succeeded)
            {
                ModelState.AddModelError(string.Empty, created.Error ?? "Create failed.");
                await LoadLookupsAsync(ct);
                return View(model);
            }

            TempData["Success"] = "Event created.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Organizer/Events/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var e = await _events.GetByIdAsync(id, ct);
            if (e is null) return NotFound();

            await LoadLookupsAsync(ct);

            var vm = new EventEditDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                Capacity = e.Capacity,
                ClubId = e.ClubId,
                EventTypeId = e.EventTypeId,
                LocationId = e.LocationId,
                ImageUrl = e.ImageUrl
            };

            return View(vm);
        }

        // POST: /Organizer/Events/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventEditDto model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await LoadLookupsAsync(ct);
                return View(model);
            }

            var updated = await _events.UpdateAsync(model, CurrentUserId, ct);
            if (!updated.Succeeded)
            {
                ModelState.AddModelError(string.Empty, updated.Error ?? "Update failed.");
                await LoadLookupsAsync(ct);
                return View(model);
            }

            TempData["Success"] = "Event updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Organizer/Events/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var e = await _events.GetByIdAsync(id, ct);
            if (e is null) return NotFound();
            return View(e); // EventDetailsDto
        }

        // POST: /Organizer/Events/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var deleted = await _events.DeleteAsync(id, CurrentUserId, ct);
            TempData[deleted.Succeeded ? "Success" : "Error"] =
                deleted.Succeeded ? "Event deleted." : (deleted.Error ?? "Delete failed.");
            return RedirectToAction(nameof(Index));
        }

        // lookups
        private async Task LoadLookupsAsync(CancellationToken ct)
        {
            var types = await _lookups.EventTypesAsync(ct);
            var clubs = await _lookups.ClubsAsync(null, ct);
            var locs  = await _lookups.LocationsAsync(ct);

            ViewBag.EventTypes = new SelectList(types, "Id", "Name");
            ViewBag.Clubs      = new SelectList(clubs, "Id", "Name");
            ViewBag.Locations  = new SelectList(locs,  "Id", "Name");
        }
    }
}
