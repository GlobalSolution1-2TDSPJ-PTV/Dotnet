using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.Controllers.ControllersWeb
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
