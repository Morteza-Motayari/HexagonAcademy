using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    public class SportClassController(ISportClassService sportClassService
        ,ISportService sportService
        ,IGymService gymService) : BaseSiteController
    {
        [Route("/Class")]
        public async Task<IActionResult> List(ClientSideFilterSportClassViewModel filter)
        {
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            ViewData["Gyms"]=await gymService.ListGymsForOptionsAsync();
            filter.TakeEntity = 6;
            var list = await sportClassService.ClientSideFilterClasses(filter);
            return View(list);
        }

        [HttpGet("/Class/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var classDetail=await sportClassService.ClientSideSportClassViewModel(slug);
            if(classDetail == null) 
                return NotFound();

            return View(classDetail);
        }        
    }
}
