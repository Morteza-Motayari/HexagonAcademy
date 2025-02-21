using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
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
