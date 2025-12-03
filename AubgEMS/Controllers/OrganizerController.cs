using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Controllers
{
    [Authorize(Policy = "RequireOrganizerOrAdmin")]
    public class OrganizerController : Controller
    {
        public IActionResult Index() => View();
        
        [HttpGet]  public IActionResult Create() => View();
        [HttpGet]  public IActionResult Edit(int id) => View();
        [HttpPost] public IActionResult Delete(int id) => RedirectToAction(nameof(Index));
    }
}