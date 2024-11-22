using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
{
    public class HomeController : UserManagementBaseSideController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
