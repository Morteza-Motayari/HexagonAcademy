using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
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
            ViewData["Title"] = Titles.SportClasses;
            return View(list);
        }

        [HttpGet("/Class/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var classDetail=await sportClassService.ClientSideSportClassViewModel(slug);
            if(classDetail == null) 
                return NotFound();

            ViewData["Title"] = classDetail.Title;
            return View(classDetail);
        }        
    }
}
