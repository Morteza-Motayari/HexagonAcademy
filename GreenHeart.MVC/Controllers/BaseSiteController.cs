using GreenHeart.Application.Statics;
using Microsoft.AspNetCore.Mvc;
using GreenHeart.Application.Extensions;

namespace GreenHeart.MVC.Controllers
{
    public class BaseSiteController : Controller
    {
        protected static string SuccessMessage = "SuccessMessage";
        protected static string ErrorMessage = "ErrorMessage";
        protected static string InfoMessage = "InfoMessage";
        protected static string WarningMessage = "WarningMessage";
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                upload.AddImageToServer(fileName, SavingPath.CkEditorImagesPath);

                return new JsonResult(new { path = SavingPath.CkEditorImagesPath + fileName });
            }

            return RedirectToAction("/Create");
        }
    }

}
