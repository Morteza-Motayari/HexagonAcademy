using GreenHeart.Application.Services.Implementation.Essays;
using GreenHeart.Application.Services.Interfaces.EssayCategorys;
using GreenHeart.Application.Services.Interfaces.Essays;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class EssayController(IEssayService essayService,IEssayCategoryService essayCategoryService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageEssays")]
        public async Task<IActionResult> List(FilterEssayViewModel filter)
        {
            var list = await essayService.FilterEssaysAsync(filter);
            ViewData["Title"] = Titles.AdminEssays;
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddEssay")]
        [HttpGet]
        public async Task<IActionResult> Create(int? EssayCategoryId)
        {
            ViewData["EssayCategories"] =await essayCategoryService.GetAllChildsEssayCategoriesAsync();
            CreateEssayViewModel essay = new CreateEssayViewModel();
            if (EssayCategoryId.HasValue)
                essay.EssayCagtegoryId = (int)EssayCategoryId.Value;

            ViewData["Title"] = Titles.AdminCreateEssay;
            return View(essay);
        }
        [AuthorizePermission("AddEssay")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEssayViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminCreateEssay;
                ViewData["EssayCategories"] = await essayCategoryService.GetAllChildsEssayCategoriesAsync();
                return View(model);
            }
            #endregion
            var result = await essayService.CreateEssayAsync(model);
            switch (result)
            {
                case CreateEssayResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.EssayAddedSuccessfully;
                    return RedirectToAction("List", "Essay", new { area = "Admin" });
                case CreateEssayResult.TitleDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.EssayTitleDuplicated;
                    break;
            }
            ViewData["EssayCategories"] = await essayCategoryService.GetAllChildsEssayCategoriesAsync();
            ViewData["Title"] = Titles.AdminCreateEssay;
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditEssay")]
        public async Task<IActionResult> Edit(int id)
        {
            var Essay = await essayService.GetEssayForEdit(id);
            if (Essay == null)
                return NotFound();
            if (Essay.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.EssayCantbeEdited;
                return RedirectToAction("List", "Essay", new { area = "Admin" });
            }
            ViewData["Title"] = Titles.AdminEditEssay;
            return View(Essay);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEssayViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    status = 204,
                    message = ErrorMessages.InsufficintInputs
                });
            }
            #endregion
            var result = await essayService.UpdateEssayAsync(model);
            switch (result)
            {
                case UpdateEssayResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.EssayUpdatedSuccessfully
                    });
                case UpdateEssayResult.TitleDuplicated:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayTitleDuplicated
                    });
                case UpdateEssayResult.EssayNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailEssay")]
        public async Task<IActionResult> Detail(int id)
        {
            var Essay = await essayService.AdminSideDetailEssayAsync(id);
            if (Essay == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailEssay;
            return View(Essay);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteEssay")]
        public async Task<IActionResult> Delete(int id, int? EssayParentId)
        {
            var result = await essayService.DeleteEssayAsync(id);
            switch (result)
            {
                case DeleteEssayResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.EssayDeletedSuccessfully;
                    break;
                case DeleteEssayResult.EssayNotFound:
                    TempData[ErrorMessage] = ErrorMessages.EssayNotFound;
                    break;
                case DeleteEssayResult.EssayAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.EssayAlreadyDeleted;
                    break;
            }
            return RedirectToAction("List", "Essay", new { area = "Admin", EssayParentId = EssayParentId });
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteEssayForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await essayService.DeleteEssayForever(id);
            switch (result)
            {
                case DeleteForeverEssayResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.EssayDeletedForeverSuccessfully;
                    break;
                case DeleteForeverEssayResult.CantDeletedNow:
                    string message = await essayService.CantDeleteEssayForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverEssayResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteEssay;
                    break;
                case DeleteForeverEssayResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.EssayNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
