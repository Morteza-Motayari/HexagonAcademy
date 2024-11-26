using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class HomeController : AdminSideController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
