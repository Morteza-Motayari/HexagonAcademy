using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class BaseSiteController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";
    }
}
