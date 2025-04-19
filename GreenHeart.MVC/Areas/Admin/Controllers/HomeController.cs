using GreenHeart.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class HomeController : AdminSideController
    {
        public IActionResult Index()
        {
            ViewData["Title"] = Titles.Home;
            return View();
        }
        public IActionResult Main()
        {
            ViewData["Title"] = Titles.Home;
            return View();
        }
    }
}
