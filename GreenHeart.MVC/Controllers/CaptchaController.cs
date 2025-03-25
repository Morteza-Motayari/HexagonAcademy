using GreenHeart.Application.Generators;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class CaptchaController : Controller
    {
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 200;
            int height = 66;
            var captchaCode = CaptchaGenerator.GenerateCaptchaCode();
            var result = CaptchaGenerator.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
    }
}
