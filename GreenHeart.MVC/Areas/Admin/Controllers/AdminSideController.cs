using GreenHeart.Application.Statics;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;


namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AuthenticateUserForAdmin]
    public class AdminSideController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";


    } 
}
