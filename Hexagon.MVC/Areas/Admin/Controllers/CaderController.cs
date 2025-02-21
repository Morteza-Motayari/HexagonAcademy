using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hexagon.MVC.Areas.Admin.Controllers
{//TODO Adding usergym to to the cader
    public class CaderController(IStaffService staffService
        , IUserService userService
        , ICertificateService certificateService
        , IRoleService roleService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageCaders")]
        public async Task<IActionResult> List(FilterCaderViewModel filter)
        {
            ViewData["Title"] = Titles.AdminCaders;
            ViewData["roles"] = await roleService.ListRolesAsync();
            var list = await staffService.FilterCadersAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddCader")]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = Titles.AdminCreateCader;
            ViewData["roles"] = await roleService.ListRolesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCaderViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminCreateCader;
                ViewData["roles"] = await roleService.ListRolesAsync();
                return View(model);
            }
            #endregion
            var result = await staffService.CreateCaderAsync(model);
            switch (result)
            {
                case CreateCaderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CaderAddedSuccessfully;
                    return RedirectToAction(nameof(List), "Cader", new { area = "Admin" });
                case CreateCaderResult.DuplicatedPosition:
                    TempData[ErrorMessage] = ErrorMessages.CaderPositionDuplicated;
                    break;
                case CreateCaderResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;
                case CreateCaderResult.InValidSalary:
                    TempData[ErrorMessage] = ErrorMessages.InvalidSalaryInput;
                    break;
                case CreateCaderResult.ExistRoleForUser:
                    TempData[ErrorMessage] = ErrorMessages.ExistRoleForUserCader;
                    break;
            }
            ViewData["Title"] = Titles.AdminCreateCader;
            ViewData["roles"] = await roleService.ListRolesAsync();
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditCader")]
        public async Task<IActionResult> Edit(int id)
        {
            var Cader = await staffService.GetCaderForEdit(id);
            if (Cader == null)
                return NotFound();
            if (Cader.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.CaderCantbeEdited;
                return RedirectToAction("List", "Cader", new { area = "Admin" });
            }
            ViewData["Title"] = Titles.AdminEditCader;
            ViewData["roles"] = await roleService.ListRolesAsync();
            return View(Cader);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCaderViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminEditCader;
                ViewData["roles"] = await roleService.ListRolesAsync();
                return View(model);
            }
            #endregion
            var result = await staffService.UpdateCaderAsync(model);
            switch (result)
            {
                case UpdateCaderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CaderUpdatedSuccessfully;
                    return RedirectToAction("List", "Cader", new { area = "Admin" });
                case UpdateCaderResult.DuplicatedPosition:
                    TempData[ErrorMessage] = ErrorMessages.CaderPositionDuplicated;
                    break;
                case UpdateCaderResult.CaderNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CaderNotFound;
                    break;
                case UpdateCaderResult.InValidSalary:
                    TempData[ErrorMessage] = ErrorMessages.InvalidSalaryInput;
                    break;
                case UpdateCaderResult.ExistRoleForUser:
                    TempData[ErrorMessage] = ErrorMessages.ExistRoleForUserCader;
                    break;
            }
            ViewData["Title"] = Titles.AdminEditCader;
            ViewData["roles"] = await roleService.ListRolesAsync();
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailCader")]
        public async Task<IActionResult> Detail(int id)
        {
            var Cader = await staffService.AdminSideDetailCaderAsync(id);
            if (Cader == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailCader;
            return View(Cader);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteCader")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await staffService.DeleteCaderAsync(id);
            switch (result)
            {
                case DeleteCaderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CaderDeletedSuccessfully;
                    break;
                case DeleteCaderResult.CaderNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CaderNotFound;
                    break;
                case DeleteCaderResult.CaderAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.CaderAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List), "Cader", new { area = "Admin" });
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteCaderForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await staffService.DeleteCaderForever(id);
            switch (result)
            {
                case DeleteForeverCaderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CaderDeletedForeverSuccessfully;
                    break;
                case DeleteForeverCaderResult.CantDeletedNow:
                    string message = await staffService.CantDeleteCaderForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverCaderResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteCader;
                    break;
                case DeleteForeverCaderResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.CaderNotFound;
                    break;
            }
            return RedirectToAction(nameof(List), "Cader", new { area = "Admin" });
        }
        #endregion

    }
}
