using Microsoft.AspNetCore.Mvc;

namespace Coders_Zone.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
