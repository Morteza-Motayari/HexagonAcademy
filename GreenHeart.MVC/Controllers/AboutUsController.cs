using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class AboutUsController(IStaffService staffService) : BaseSiteController
    {
        [Route("/AboutUs")]
        public async Task<IActionResult> AboutUs()
        {           
            var Caders=await staffService.GetCadersForAbouUsPageAsync();
            ViewData["Title"] = Titles.AboutUs;
            return View(Caders);
        }
    }
}
