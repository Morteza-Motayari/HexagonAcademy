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
            return PartialView("_AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
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

            var result = await roleService.CreateRoleAsync(model);
            switch (result)
            {
                case CreateRoleResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.RoleAddedSuccessfully
                    });
                case CreateRoleResult.DupliactedRole:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.RoleTitleDuplicated
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int roleId)
        {
            var role = await roleService.GetRoleForEdit(roleId);
            return PartialView("_EditRole",role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleViewModel model)
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

            var result = await roleService.UpdateRoleAsync(model);
            switch (result)
            {
                case UpdateRoleResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.RoleUpdatedSuccessfully
                    });
                case UpdateRoleResult.DupliactedRole:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.RoleTitleDuplicated
                    });
                case UpdateRoleResult.NotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.RoleNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
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
