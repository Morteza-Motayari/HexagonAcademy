using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class UserController : AdminSideController
    {
        #region List
        public IActionResult List()
        {
            return View();
        }
        #endregion
    }
}
