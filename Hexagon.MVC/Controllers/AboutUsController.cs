using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    public class AboutUsController(IStaffService staffService) : BaseSiteController
    {
        [Route("/AboutUs")]
        public async Task<IActionResult> AboutUs()
        {
            var Caders=await staffService.GetCadersForAbouUsPageAsync();
            return View(Caders);
        }
    }
}
