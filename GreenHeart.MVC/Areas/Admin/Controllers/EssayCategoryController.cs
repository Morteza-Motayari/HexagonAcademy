using GreenHeart.Application.Services.Interfaces.EssayCategorys;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class EssayCategoryController(IEssayCategoryService essayCategoryService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageEssayCategories")]
        public async Task<IActionResult> List(FilterEssayCategoryViewModel filter, int essayCategoryParentId)
        {
            ViewData["essayCategoryParentId"] = essayCategoryParentId;
            var list = await essayCategoryService.FilterEssayCategorysAsync(filter, essayCategoryParentId);
            ViewData["Title"] = Titles.AdminEssayCategorys;
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddEssayCategory")]
        public async Task<IActionResult> Create(int? EssayCategoryId)
        {
            ViewData["EssayCategoryId"] = EssayCategoryId;
            return PartialView("_AddEssayCategory");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEssayCategoryViewModel model)
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
            var result = await essayCategoryService.CreateEssayCategoryAsync(model);
            switch (result)
            {
                case CreateEssayCategoryResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.EssayCategoryAddedSuccessfully
                    });
                case CreateEssayCategoryResult.TitleDuplicated:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayCategoryTitleDuplicated
                    });
                case CreateEssayCategoryResult.EssayCategoryHasEssaysAndCantHaveChildCategory:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayCategoryHasEssaysAndCantHaveChildCategory
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditEssayCategory")]
        public async Task<IActionResult> Edit(int id)
        {
            var EssayCategory = await essayCategoryService.GetEssayCategoryForEdit(id);
            if (EssayCategory == null)
                return NotFound();
            if (EssayCategory.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.EssayCategoryCantbeEdited;
                return RedirectToAction("List", "EssayCategory", new { area = "Admin"});
            }
            return PartialView("_EditEssayCategory", EssayCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEssayCategoryViewModel model)
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
            var result = await essayCategoryService.UpdateEssayCategoryAsync(model);
            switch (result)
            {
                case UpdateEssayCategoryResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.EssayCategoryUpdatedSuccessfully
                    });
                case UpdateEssayCategoryResult.TitleDuplicated:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayCategoryTitleDuplicated
                    });
                case UpdateEssayCategoryResult.EssayCategoryNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.EssayCategoryNotFound
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
        [AuthorizePermission("DetailEssayCategory")]
        public async Task<IActionResult> Detail(int id)
        {
            var EssayCategory = await essayCategoryService.AdminSideDetailEssayCategoryAsync(id);
            if (EssayCategory == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailEssayCategory;
            return View(EssayCategory);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteEssayCategory")]
        public async Task<IActionResult> Delete(int id, int? EssaYCategoryParentId)
        {
            var result = await essayCategoryService.DeleteEssayCategoryAsync(id);
            switch (result)
            {
                case DeleteEssayCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.EssayCategoryDeletedSuccessfully;
                    break;
                case DeleteEssayCategoryResult.EssayCategoryNotFound:
                    TempData[ErrorMessage] = ErrorMessages.EssayCategoryNotFound;
                    break;
                case DeleteEssayCategoryResult.EssayCategoryAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.EssayCategoryAlreadyDeleted;
                    break;
            }
            return RedirectToAction("List", "EssayCategory", new { area = "Admin", essayCategoryParentId = EssaYCategoryParentId });
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteEssayCategoryForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await essayCategoryService.DeleteEssayCategoryForever(id);
            switch (result)
            {
                case DeleteForeverEssayCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.EssayCategoryDeletedForeverSuccessfully;
                    break;
                case DeleteForeverEssayCategoryResult.CantDeletedNow:
                    string message = await essayCategoryService.CantDeleteEssayCategoryForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverEssayCategoryResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteEssayCategory;
                    break;
                case DeleteForeverEssayCategoryResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.EssayCategoryNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
