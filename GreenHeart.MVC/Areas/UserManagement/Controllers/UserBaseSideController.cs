using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    [Authorize]
    public class UserManagementBaseSideController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";
    }
}
