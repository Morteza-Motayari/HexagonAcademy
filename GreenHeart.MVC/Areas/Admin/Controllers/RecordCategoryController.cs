using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using GreenHeart.Domain.ViewModels.Records.RecordCategories;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class RecordCategoryController(IRecordCategoryService recordCategoryService
        ,IStaffService staffService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageRecordCategories")]
        public async Task<IActionResult> List(int staffId,FilterRecordCategoryViewModel filter)
        {
            ViewData["Title"] = Titles.AdminRecordCategories;
            ViewData["staffName"] = await staffService.GetStaffNameAsync(staffId);
            ViewData["staffId"] = staffId;
            ViewData["userId"] = await staffService.GetUserIdByStaffIdAsync(staffId);
            var category=await recordCategoryService.FilterRecordCategoryesAsync(staffId, filter);
            return View(category);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddRecordCategory")]
        public async Task<IActionResult> Create(int staffId)
        {
            ViewData["staffId"] = staffId;
            ViewData["staffName"] = await staffService.GetStaffNameAsync(staffId);
            ViewData["Title"] = Titles.AdminCreateRecordCategory;
            return PartialView("_Create",new CreateRecordCategoryViewModel
            {
                StaffId = staffId
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecordCategoryViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["staffId"] = model.StaffId;
                ViewData["Title"] = Titles.AdminCreateRecordCategory;
                return Ok(new
                {
                    status = 204,
                    message = ErrorMessages.InsufficintInputs
                });
            }
            #endregion
            var result = await recordCategoryService.CreateRecordCategoryAsync(model);
            switch (result)
            {
                case CreateRecordCategoryResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.RecordCategoryAddedSuccessfully
                    });
                case CreateRecordCategoryResult.CategoryExisted:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.RecordCategoryExisted
                    });
            }
            ViewData["staffId"] = model.StaffId;
            ViewData["Title"] = Titles.AdminCreateRecordCategory;
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditRecordCategory")]
        public async Task<IActionResult> Edit(int id)
        {
            var recordcategory = await recordCategoryService.GetRecordCategoryForEdit(id);
            if (recordcategory == null)
                return NotFound();
            if (recordcategory.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.RecordCategoryCantbeEdited;
                return RedirectToAction("List", "RecordCategory", new { area = "Admin" });
            }
            ViewData["Title"] = Titles.AdminEditRecordCategory;
            return PartialView("_Edit",recordcategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRecordCategoryViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminEditRecordCategory;
                return View(model);
            }
            #endregion

            var result = await recordCategoryService.UpdateRecordCategoryAsync(model);
            switch (result)
            {
                case UpdateRecordCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RecordCategoryUpdatedSuccessfully;
                    return RedirectToAction(nameof(Create), "RecordCategory", new { staffId = model.StaffId});
                case UpdateRecordCategoryResult.RecordCategoryNotFound:
                    TempData[ErrorMessage] = ErrorMessages.RecordCategoryNotFound;
                    return RedirectToAction(nameof(List), "RecordCategory");
                case UpdateRecordCategoryResult.RecordCategoryExisted:
                    TempData[ErrorMessage] = ErrorMessages.RecordCategoryExisted;
                    return RedirectToAction(nameof(List), "RecordCategory");
            }
            ViewData["Title"] = Titles.AdminEditRecordCategory;
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailRecordCategory")]
        public async Task<IActionResult> Detail(int id)
        {
            var recordcategory = await recordCategoryService.AdminSideDetailRecordCategoryAsync(id);
            if (recordcategory == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailRecordCategory+" "+recordcategory.Title;
            return View(recordcategory);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteRecordCategory")]
        public async Task<IActionResult> Delete(int id,int StaffId)
        {
            var result = await recordCategoryService.DeleteRecordCategoryAsync(id);
            switch (result)
            {
                case DeleteRecordCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RecordCategoryDeletedSuccessfully;
                    break;
                case DeleteRecordCategoryResult.RecordCategoryNotFound:
                    TempData[ErrorMessage] = ErrorMessages.RecordCategoryNotFound;
                    break;
                case DeleteRecordCategoryResult.RecordCategoryAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.RecordCategoryAlreadyDeleted;
                    break;
            }
            return RedirectToAction("List", "RecordCategory", new { area = "Admin", staffId = StaffId });
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteRecordCategoryForever")]
        public async Task<IActionResult> DeleteForever(int id,int StaffId)
        {
            var result = await recordCategoryService.DeleteRecordCategoryForever(id);
            switch (result)
            {
                case DeleteForeverRecordCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RecordCategoryDeletedForeverSuccessfully;
                    break;
                case DeleteForeverRecordCategoryResult.CantDeletedNow:
                    string message = await recordCategoryService.CantDeleteRecordCategoryForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverRecordCategoryResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteRecordCategory;
                    break;
                case DeleteForeverRecordCategoryResult.RecordCategoryNotFound:
                    TempData[ErrorMessage] = ErrorMessages.RecordCategoryNotFound;
                    break;
            }
            return RedirectToAction("List", "RecordCategory", new { area = "Admin", staffId = StaffId });
        }
        #endregion
    }
}
