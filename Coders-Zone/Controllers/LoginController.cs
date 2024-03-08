using Microsoft.AspNetCore.Mvc;

namespace Coders_Zone.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
