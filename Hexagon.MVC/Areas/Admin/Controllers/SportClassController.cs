using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class SportClassController(ISportClassService SportClassService,
        IGymService gymService,
        ISportService sportService,
        IStaffService staffService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageSportClasses")]
        public async Task<IActionResult> List(FilterSportClassViewModel filter)
        {
            ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            var list = await SportClassService.FilterSportClassesAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddSportClass")]
        public async Task<IActionResult> Create()
        {
            ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSportClassViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
                ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
                return View(model);
            }
            #endregion
            var result = await SportClassService.CreateSportClassAsync(model);
            switch (result)
            {
                case CreateSportClassResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportClassAddedSuccessfully;
                    return RedirectToAction("List", "SportClass", new { area = "Admin" });
                case CreateSportClassResult.InvalidDateTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidStartDateTimeInput;
                    break;
                case CreateSportClassResult.InvalidEndTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidEndTime;
                    break;
            }
            ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditSportClass")]
        public async Task<IActionResult> Edit(int id)
        {
            var SportClass = await SportClassService.GetSportClassForEdit(id);
            if (SportClass == null)
                return NotFound();
            if (SportClass.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.SportClassCantbeEdited;
                return RedirectToAction("List", "SportClass", new { area = "Admin" });
            }
            ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            ViewData["Trainers"] = await staffService.ListTrainerForEditItemsAsync(SportClass.Gender, SportClass.SportId);
            return View(SportClass);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSportClassViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
                ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
                ViewData["Trainers"] = await staffService.ListTrainerForEditItemsAsync(model.Gender,model.SportId);
                return View(model);
            }
            #endregion
            var result = await SportClassService.UpdateSportClassAsync(model);
            switch (result)
            {
                case UpdateSportClassResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportClassUpdatedSuccessfully;                   
                    return RedirectToAction("List", "SportClass", new { area = "Admin" });
                case UpdateSportClassResult.SportClassNotFound:
                    TempData[ErrorMessage]=ErrorMessages.SportClassNotFound;
                    break;
                case UpdateSportClassResult.InvalidDateTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidStartDateTimeInput;
                    break;
                case UpdateSportClassResult.InvalidEndTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidEndTime;
                    break;
            }
            ViewData["Gyms"] = await gymService.ListGymsForOptionsAsync();
            ViewData["Sports"] = await sportService.ListSportsForOptionsAsync();
            ViewData["Trainers"] = await staffService.ListTrainerForEditItemsAsync(model.Gender, model.SportId);
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailSportClass")]
        public async Task<IActionResult> Detail(int id)
        {
            var SportClass = await SportClassService.AdminSideDetailSportClassAsync(id);
            if (SportClass == null)
                return NotFound();

            return View(SportClass);
        }
        #endregion
        //TODO check out that all entities implement case AlreadyDeleted
        #region Delete
        [AuthorizePermission("DeleteSportClass")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await SportClassService.DeleteSportClassAsync(id);
            switch (result)
            {
                case DeleteSportClassResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportClassDeletedSuccessfully;
                    break;
                case DeleteSportClassResult.SportClassNotFound:
                    TempData[ErrorMessage] = ErrorMessages.SportClassNotFound;
                    break;
                case DeleteSportClassResult.SportClassAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.SportClassAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteSportClassForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await SportClassService.DeleteSportClassForever(id);
            switch (result)
            {
                case DeleteForeverSportClassResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportClassDeletedForeverSuccessfully;
                    break;
                case DeleteForeverSportClassResult.CantDeletedNow:
                    string message = await SportClassService.CantDeleteSportClassForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverSportClassResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteSportClass;
                    break;
                case DeleteForeverSportClassResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.SportClassNotFound;
                    break;
            }
            return RedirectToAction(nameof(List), "SportClass", new {area= "Admin" });
        }
        #endregion
    }
}
