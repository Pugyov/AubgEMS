using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Controllers
{
    [Authorize(Policy = "RequireSignedIn")]
    public class MyController : Controller
    {
        public IActionResult Events() => View(); // later: joined ∪ created
    }
}