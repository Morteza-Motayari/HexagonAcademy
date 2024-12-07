using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.GymGalleries;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class GymGalleryController(IGymGalleryService gymGalleryService) : AdminSideController
    {
        //TODO Making the Table responsive in sm-size
        [HttpGet]
        [AuthorizePermission("GalleryGym")]
        public async Task<IActionResult> Gallery(int gymId)
        {
            if(!await gymGalleryService.GymExistForGalleyAsync(gymId))
            {
                TempData[ErrorMessage] = ErrorMessages.GymNotFound;
                return RedirectToAction("List","Gym", new { area = "Admin" });
            }
            ViewData["GymGallery"]=await gymGalleryService.ListGymGallerysAsync(gymId);
            return View(new CreateGymGalleryViewModel
            {
                GymId = gymId
            });
        }
        [HttpPost]
        [AuthorizePermission("AddGalleryGym")]
        public async Task<IActionResult> Gallery(CreateGymGalleryViewModel model)
        {
            #region Validations
            if(!ModelState.IsValid)
            {
                ViewData["GymGallery"] = await gymGalleryService.ListGymGallerysAsync(model.GymId);
                return View(model);
            }
                
            #endregion
            var result=await gymGalleryService.CreateGymGalleryAsync(model);
            switch(result)
            {
                case CreateGymGalleryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.GymGalleryAddedSuccessfully;
                    return RedirectToAction(nameof(Gallery), new { gymId =model.GymId});
                case CreateGymGalleryResult.ImageNull:
                    TempData[ErrorMessage] = ErrorMessages.GymGalleryNull;
                    break;
                case CreateGymGalleryResult.MaxImagesForGym:
                    TempData[ErrorMessage] = ErrorMessages.MaxImagesForGym;
                    break;
            }
            ViewData["GymGallery"] = await gymGalleryService.ListGymGallerysAsync(model.GymId);
            return View(model);
        }
        [AuthorizePermission("DeleteGalleryGym")]
        public async Task DeleteImage(int id)
        {
            if(id==0)
            {
                TempData[WarningMessage] = WarningMessages.GymGalleryIdZero;
            }
            await gymGalleryService.DeleteGymGalleryAsync(id);
        }
    }
}
