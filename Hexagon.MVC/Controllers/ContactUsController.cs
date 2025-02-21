using Hexagon.Application.Services.Interfaces.Contact_Us;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Contact_Us;
using Hexagon.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
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
