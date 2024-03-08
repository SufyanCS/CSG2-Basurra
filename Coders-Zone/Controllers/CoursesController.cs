using Microsoft.AspNetCore.Mvc;

namespace Coders_Zone.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
