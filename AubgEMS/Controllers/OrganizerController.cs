using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Controllers
{
    [Authorize(Policy = "RequireOrganizerOrAdmin")]
    public class OrganizerController : Controller
    {
        // Manage landing (later: list “events I created”)
        public IActionResult Index() => View();

        // Stubs I will wire to services in Phase 3/4
        [HttpGet]  public IActionResult Create() => View();
        [HttpGet]  public IActionResult Edit(int id) => View();
        [HttpPost] public IActionResult Delete(int id) => RedirectToAction(nameof(Index));
    }
}