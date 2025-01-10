using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
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
