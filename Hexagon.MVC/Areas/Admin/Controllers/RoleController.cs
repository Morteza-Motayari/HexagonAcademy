using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Users.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class RoleController(IRoleService roleService) : AdminSideController
    {
        #region List
        public async Task<IActionResult> List(FilterRoleViewModel filter)
        {
            var roles = await roleService.FilterRolesAsync(filter);
            return View(roles);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await roleService.CreateRoleAsync(model);
            switch (result)
            {
                case CreateRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleAddedSuccessfully;
                    return RedirectToAction(nameof(List));
                case CreateRoleResult.DupliactedRole:
                    TempData[ErrorMessage] = ErrorMessages.RoleTitleDuplicated;
                    break;
            }
            return View();
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int roleId)
        {
            var role = await roleService.GetRoleForEdit(roleId);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await roleService.UpdateRoleAsync(model);
            switch (result)
            {
                case UpdateRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleAddedSuccessfully;
                    return RedirectToAction(nameof(List));
                case UpdateRoleResult.DupliactedRole:
                    TempData[ErrorMessage] = ErrorMessages.RoleTitleDuplicated;
                    break;
                case UpdateRoleResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
                    break;
            }
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result = await roleService.DeleteRoleAsync(id);
            switch (result)
            {
                case DeleteRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RoleDeletedSuccessfully;
                    break;
                case DeleteRoleResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
