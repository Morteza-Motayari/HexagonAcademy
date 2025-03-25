using GreenHeart.Application.Services.Interfaces.Contact_Us;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Contact_Us;
using GreenHeart.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : BaseSiteController
    {
        [HttpGet(template:"Contact-Us")]
        public IActionResult ContactUs()
        {
            ViewData["Title"] = Titles.ContactUs;
            return View();
        }
        [HttpPost(template: "Contact-Us")]
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.ContactUs;
                return View(model);
            }
            #endregion
            model.IP = HttpContext.GettingIP();
            var result =await contactUsService.CreateContactUsAsync(model);
            switch (result)
            {
                case CreateContactUsResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ContactUsAddedSuccessfully;
                    return Redirect("/");
            }
            ViewData["Title"] = Titles.ContactUs;
            return View(model);
        }

    }
}
