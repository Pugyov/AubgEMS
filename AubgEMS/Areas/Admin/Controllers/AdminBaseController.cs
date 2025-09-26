using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")] // uses the policy we added earlier
    public abstract class AdminBaseController : Controller { }
}