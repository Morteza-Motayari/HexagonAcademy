using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.EssayCategorys;
using GreenHeart.Application.Services.Interfaces.Essays;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Controllers
{
    public class EssayController(IEssayService essayService
        ,IEssayCategoryService essayCategoryService) : BaseSiteController
    {
        [Route("/Essay")]
        public async Task<IActionResult> List(ClientSideFilterEssayViewModel filter)
        {
            ViewData["EssayCategories"] = await essayCategoryService.GetAllChildsEssayCategoriesAsync();
            filter.TakeEntity = 6;
            var list = await essayService.FilterClientSideEssayAsync(filter);
            ViewData["Title"] = Titles.SportClasses;
            return View(list);
        }
        [HttpGet("/Essay/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var essayDetail = await essayService.GetClientSideEssayBySlugAsync(slug);
            if (essayDetail == null)
                return NotFound();

            ViewData["Title"] = essayDetail.Title;
            return View(essayDetail);
        }

    }
}
