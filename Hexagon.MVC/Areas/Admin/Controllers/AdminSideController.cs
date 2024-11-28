using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminSideController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";
    }
}
