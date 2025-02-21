using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class GymController(IGymService gymService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageGyms")]
        public async Task<IActionResult> List(FilterGymViewModel filter)
        {
            var list=await gymService.FilterGymsAsync(filter);
            ViewData["Title"] = Titles.AdminGyms;
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddGym")]
        public IActionResult Create()
        {
            ViewData["Title"] = Titles.AdminCreateGym;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGymViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminCreateGym;
                return View(model);
            }
            #endregion
            var result = await gymService.CreateGymAsync(model);
            switch (result)
            {
                case CreateGymResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.GymAddedSuccessfully;
                    return RedirectToAction(nameof(List), "Gym", new { area = "Admin" });
                case CreateGymResult.DuplicatedConstantPhone:
                    TempData[ErrorMessage] = ErrorMessages.GymConstatntPhoneNumberDuplicated;
                    break;
            }
            ViewData["Title"] = Titles.AdminCreateGym;
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditGym")]
        public async Task<IActionResult> Edit(int id)
        {
            var gym = await gymService.GetGymForEdit(id);
            if (gym == null)
                return NotFound();
            if (gym.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.GymCantbeEdited;
                return RedirectToAction("List", "Gym", new { area = "Admin" });
            }
            ViewData["Title"] = Titles.AdminEditGym;
            return View(gym);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateGymViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminEditGym;
                return View(model);
            }
            #endregion
            var result = await gymService.UpdateGymAsync(model);
            switch (result)
            {
                case UpdateGymResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.GymUpdatedSuccessfully;
                    return RedirectToAction(nameof(List), "Gym", new { area = "Admin" });
                case UpdateGymResult.DuplicatedConstantPhone:
                    TempData[ErrorMessage] = ErrorMessages.GymConstatntPhoneNumberDuplicated;
                    break;
                case UpdateGymResult.GymNotFound:
                    TempData[ErrorMessage] = ErrorMessages.GymNotFound;
                    break;
            }
            ViewData["Title"] = Titles.AdminEditGym;
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailGym")]
        public async Task<IActionResult> Detail(int id)
        {
            var Gym = await gymService.AdminSideDetailGymAsync(id);
            if (Gym == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailGym;
            return View(Gym);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteGym")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await gymService.DeleteGymAsync(id);
            switch (result)
            {
                case DeleteGymResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.GymDeletedSuccessfully;
                    break;
                case DeleteGymResult.GymNotFound:
                    TempData[ErrorMessage] = ErrorMessages.GymNotFound;
                    break;
                case DeleteGymResult.UserAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.GymAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteGymForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await gymService.DeleteGymForever(id);
            switch (result)
            {
                case DeleteForeverGymResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.GymDeletedForeverSuccessfully;
                    break;
                case DeleteForeverGymResult.CantDeletedNow:
                    string message = await gymService.CantDeleteGymForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverGymResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteGym;
                    break;
                case DeleteForeverGymResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.GymNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
