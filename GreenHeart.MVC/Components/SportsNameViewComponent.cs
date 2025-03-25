using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Components
{
    public class SportsNameViewComponent(ISportService sportService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sports=await sportService.GetActiveSportNameAsync();
            return View("SportsName", sports);
        }
    }
}
