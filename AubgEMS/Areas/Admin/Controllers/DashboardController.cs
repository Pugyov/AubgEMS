using Microsoft.AspNetCore.Mvc;

namespace AubgEMS.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index() => View();
    }
}