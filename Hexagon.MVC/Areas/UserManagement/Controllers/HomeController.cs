using Hexagon.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
{
    public class HomeController : UserManagementBaseSideController
    {
        public IActionResult Index()
        {
            ViewData["Title"] = Titles.DashBoard;
            return View();
        }
    }
}
