using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Users.Users;
using Microsoft.AspNetCore.Mvc;
using GreenHeart.Application.Convertors;

namespace GreenHeart.MVC.Areas.UserManagement.Controllers
{
    public class UserController(IAccountService accountService) : UserManagementBaseSideController
    {
        #region Actions

        #region Edit Profile
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            ClientSideUpdateUserViewModel? model=await accountService.GetUserForUpdateClientSide(User.GetUserId());
            if (model == null)
                return NotFound();
            ViewData["Title"] = Titles.UserInfo;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ClientSideUpdateUserViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ErrorMessages.ErrorInUpdateUserOccured;
                return View(model);
            }                
            #endregion
            var result = await accountService.UpdateUserClientSideAsync(model);
            switch(result)
            {
                case ClientSideUpdateUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.PersonalUserInfoUpdatedSuccessfully;
                    return RedirectToAction("Index", "Home", "UserManagement");
                case ClientSideUpdateUserResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotExisted;
                    break;
                case ClientSideUpdateUserResult.InvalidDateTime:
                    TempData[ErrorMessage] = ErrorMessages.InvalidDateTimeInput;
                    break;
            }
            return View(model);
        }
        #endregion

        #endregion
    }
}
