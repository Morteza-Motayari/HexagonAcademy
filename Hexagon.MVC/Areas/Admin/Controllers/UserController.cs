using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class UserController(IUserService userService, IRoleService roleService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageUsers")]
        public async Task<IActionResult> List(FilterUserViewModel filter)
        {
            var list = await userService.FilterUsersAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddUser")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await userService.CreateUserAsync(model);
            switch (result)
            {
                case CreateUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UserAddedSuccessfully;
                    return RedirectToAction(nameof(List), "User", new { area = "Admin" });
                case CreateUserResult.MobileDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.UserPhoneNumberDuplicated;
                    break;
                case CreateUserResult.InvalidDateTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidDateTimeInput;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditUser")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userService.GetUserForEdit(id);
            if (user == null)
                return NotFound();
            if (user.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.UserCantbeEdited;
                return RedirectToAction("List", "User", new {area="Admin"});
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await userService.UpdateUserAsync(model);
            switch (result)
            {
                case UpdateUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UserUpdatedSuccessfully;
                    return RedirectToAction(nameof(List), "User", "Admin");
                case UpdateUserResult.MobileDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.UserPhoneNumberDuplicated;
                    break;
                case UpdateUserResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserPhoneNumberDuplicated;
                    break;
                case UpdateUserResult.InvalidDateTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidDateTimeInput;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailUser")]
        public async Task<IActionResult> Detail(int id)
        {
            var user = await userService.AdminSideDetailUserAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userService.DeleteUserAsync(id);
            switch (result)
            {
                case DeleteUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UserDeletedSuccessfully;
                    break;
                case DeleteUserResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;
                case DeleteUserResult.UserAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.UserAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Change Password
        [AuthorizePermission("ChangeUserPassword")]
        public async Task<IActionResult> ChangePassword(int id)
        {
            var user=await userService.AdminGetUserForChangePassword(id);
            if (user == null) return NotFound();

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(AdminChagePasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await userService.AdminChangeUserPasswordAsync(model);
            switch (result)
            {
                case AdminChagePasswordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UserPasswordChangedSuccessfully;
                    return RedirectToAction("List", "User", new { area = "Admin" });
                case AdminChagePasswordResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;
            }
            return View(model);
        }
        #endregion

        #region User Option
        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var data = await userService.ListUsersForItemsAsync(term);
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }
        #endregion
    }
}
