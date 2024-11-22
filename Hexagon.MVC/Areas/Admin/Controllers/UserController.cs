using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class UserController(IAccountService accountService) : AdminSideController
    {
        #region List
        public IActionResult List()
        {
            return View();
        }
        #endregion
    }
}
