using Hexagon.Application.Services.Interfaces.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
{
    public class SportExistedViewComponent(ISportService sportService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sports=await sportService.GetSportExistedAsync();
            return View("SportExisted", sports);
        }
    }
}
