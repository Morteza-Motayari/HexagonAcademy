using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    public class BaseSiteController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";
    }
}
