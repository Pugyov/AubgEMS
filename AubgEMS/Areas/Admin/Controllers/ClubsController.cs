using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Clubs;
using AubgEMS.Core.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AubgEMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class ClubsController : Controller
    {
        private readonly IClubService _clubs;
        private readonly ILookupService _lookups;

        public ClubsController(IClubService clubs, ILookupService lookups)
        {
            _clubs = clubs;
            _lookups = lookups;
        }

        // GET: /Admin/Clubs?departmentId=&search=&page=1&pageSize=10
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

        // GET: /Admin/Clubs/Create
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            await LoadDepartments(ct);
            return View(new ClubEditDto());
        }

        // POST: /Admin/Clubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClubEditDto model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await LoadDepartments(ct);
                return View(model);
            }

            var id = await _clubs.CreateAsync(model, ct);
            TempData["Success"] = $"Club created (Id {id}).";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Clubs/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var vm = await _clubs.GetForEditAsync(id, ct);
            if (vm is null) return NotFound();

            ViewBag.ClubId = id;
            await LoadDepartments(ct);
            return View(vm);
        }

        // POST: /Admin/Clubs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClubEditDto model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClubId = id;
                await LoadDepartments(ct);
                return View(model);
            }

            var ok = await _clubs.UpdateAsync(id, model, ct);
            TempData[ok ? "Success" : "Error"] = ok ? "Club updated." : "Update failed.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Clubs/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var club = await _clubs.GetDetailsAsync(id, ct);
            if (club is null) return NotFound();
            return View(club);
        }

        // POST: /Admin/Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var ok = await _clubs.DeleteAsync(id, ct); // service already guards when there are events
            TempData[ok ? "Success" : "Error"] = ok
                ? "Club deleted."
                : "Delete failed. The club might have events.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDepartments(CancellationToken ct)
        {
            var depts = await _lookups.DepartmentsAsync(ct);
            ViewBag.Departments = new SelectList(depts, "Id", "Name");
        }
    }
}