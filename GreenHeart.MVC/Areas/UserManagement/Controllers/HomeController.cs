using GreenHeart.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.UserManagement.Controllers
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
