using Hexagon.Application.Services.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
{
    public class SportExistedViewComponent(ISportService sportService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int sportId)
        {
            var sports=await sportService.GetSportExistedAsync();
            ViewData["sportId"] = sportId;
            return View("SportExisted", sports);
        }
    }
}
