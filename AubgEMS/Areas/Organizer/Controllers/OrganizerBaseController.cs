using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AubgEMS.Areas.Organizer.Controllers
{
    [Area("Organizer")]
    [Authorize(Policy = "RequireOrganizerOrAdmin")]
    public abstract class OrganizerBaseController : Controller
    {
        protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}