using GreenHeart.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            
            if (statusCode == 404)
            {
                ViewData["Title"] = Titles.ContentNotFound;
                return View("404");
            }
            ViewData["Title"] = Titles.ContentNotFound;
            return View("Error");
        }
    }
}
