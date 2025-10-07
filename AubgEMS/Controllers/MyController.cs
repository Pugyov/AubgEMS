using System.Security.Claims;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Controllers
{
    [Authorize(Policy = "RequireSignedIn")]
    public class MyController : Controller
    {
        private readonly IAttendanceService _attendance;

        public MyController(IAttendanceService attendance)
        {
            _attendance = attendance;
        }

        // GET: /My/Events?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Events(int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            // sanitize paging
            if (page < 1) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            // [Authorize] ensures a user exists; NameIdentifier is safe to read
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _attendance.GetMyEventsAsync(
                userId,
                new PageQuery(page, pageSize),
                ct
            );

            return View(result); // Views/My/Events.cshtml
        }
    }
}