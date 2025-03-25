using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.Banners;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Banners;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class BannerController(IBannerService bannerService) : AdminSideController
    {
        #region List
        [HttpGet]
        [Route("/Admin/Banner")]
        [AuthorizePermission("ManageBanners")]
        public async Task<IActionResult> Banner()
        {
            ViewData["Title"]=Titles.AdminBanners;
            ViewData["Banners"]=await bannerService.ListBannersAsync();
            return View();
        }
        [HttpPost]
        [Route("/Admin/Banner")]
        [AuthorizePermission("AddBanner")]
        public async Task<IActionResult> Banner(CreateBannerViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminBanners;
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
            ViewData["Title"] = Titles.AdminBanners;
            ViewData["Banners"] = await bannerService.ListBannersAsync();
            return View(model);
        }
        #endregion
        [AuthorizePermission("DeleteBanner")]
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
