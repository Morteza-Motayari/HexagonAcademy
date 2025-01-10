using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class RoleController(IRoleService roleService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageRoles")]
        public async Task<IActionResult> List(FilterRoleViewModel filter)
        {           
            var roles = await roleService.FilterRolesAsync(filter);
            return View(roles);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddRole")]
        [HttpGet]        
        public async Task<IActionResult> Create()
        {
            ViewData["Permissions"] = await roleService.GetAllPermmisions();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = await roleService.GetAllPermmisions();
                return View(model);
            }
            #endregion

            var result = await roleService.CreateRoleAsync(model);
            switch (result)
            {
                case CreateRoleResult.Success:                    
                    TempData[SuccessMessage] = SuccessMessages.RoleAddedSuccessfully;
                    return RedirectToAction(nameof(List), "Role", new { area = "Admin" });
                case CreateRoleResult.DupliactedRole:
                    TempData[ErrorMessage] = ErrorMessages.RoleTitleDuplicated;
                    break;
            }
            ViewData["Permissions"] = await roleService.GetAllPermmisions();
            return View(model);
        }
        #endregion

        #region Update
        [AuthorizePermission("EditRole")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            
            var role = await roleService.GetRoleForEdit(id);
            if (role == null)
                return NotFound();
            if (role.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.RoleCantbeEdited;
                return RedirectToAction("List", "Role", new { area = "Admin" });
            }
            ViewData["Permissions"] = await roleService.GetAllPermmisions();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = await roleService.GetAllPermmisions();
                return View(model);
            }
            #endregion

            var result = await roleService.UpdateRoleAsync(model);
            switch (result)
            {
                case UpdateRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleUpdatedSuccessfully;
                    return RedirectToAction(nameof(List), "Role", new { area = "Admin" });
                case UpdateRoleResult.DupliactedRole:
                    TempData[ErrorMessage] = ErrorMessages.RoleTitleDuplicated;
                    break;
                case UpdateRoleResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
                    break;
            }
            ViewData["Permissions"] = await roleService.GetAllPermmisions();
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailRole")]
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Permissions"] = await roleService.GetAllPermmisions();
            var Role = await roleService.AdminSideDetailRoleAsync(id);
            if (Role == null)
                return NotFound();

            return View(Role);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteRole")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await roleService.DeleteRoleAsync(id);
            switch (result)
            {
                case DeleteRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleDeletedSuccessfully;
                    break;
                case DeleteRoleResult.RoleAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.RoleAlreadyDeleted;
                    break;
                case DeleteRoleResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteRoleForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await roleService.DeleteRoleForever(id);
            switch (result)
            {
                case DeleteForeverRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleDeletedForeverSuccessfully;
                    break;
                case DeleteForeverRoleResult.CantDeletedNow:
                    string message=await roleService.CantDeleteRoleForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverRoleResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteRole;
                    break;
                case DeleteForeverRoleResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
