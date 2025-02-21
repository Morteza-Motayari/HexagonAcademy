using Hexagon.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class HomeController : AdminSideController
    {
        public IActionResult Index()
        {
            ViewData["Title"] = Titles.Home;
            return View();
        }
    }
}
