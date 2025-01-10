using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Banners;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Banners;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class BannerController(IBannerService bannerService) : AdminSideController
    {
        #region List
        [HttpGet]
        [Route("/Admin/Banner")]
        public async Task<IActionResult> Banner()
        {
            ViewData["Banners"]=await bannerService.ListBannersAsync();
            return View();
        }
        [HttpPost]
        [Route("/Admin/Banner")]
        public async Task<IActionResult> Banner(CreateBannerViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Banners"] = await bannerService.ListBannersAsync();
                return View(model);
            }
            #endregion

            var result=await bannerService.CreateBannerAsync(model);
            switch (result)
            {
                case CreateBannerResult.Success:
                    return RedirectToAction(nameof(Banner), "Banner");
                case CreateBannerResult.DuplicatedBannerName:
                    TempData[ErrorMessage] = ErrorMessages.BannerNameDuplicated;
                    break;
                case CreateBannerResult.MaximumBannerReached:
                    TempData[ErrorMessage] = ErrorMessages.BannerReachedMaximumAmount;
                    break;
            }
            ViewData["Banners"] = await bannerService.ListBannersAsync();
            return View(model);
        }
        #endregion

        public async Task Delete(int id)
        {
            if (id == 0)
            {
                TempData[WarningMessage] = WarningMessages.GymGalleryIdZero;
            }
            else
            {
                await bannerService.DeleteBannerAsync(id);
            }
        }
    }
}
