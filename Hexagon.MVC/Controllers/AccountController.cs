using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hexagon.MVC.Controllers
{
    public class AccountController(IAccountService accountService) : BaseSiteController
    {
        #region Actions

        #region Register
        [HttpGet(template: "/register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", "UserManagement");
            return View();
        }
        [HttpPost(template: "/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result = await accountService.RegisterAsync(model);
            switch (result)
            {
                case RegisterResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.RigesterDoneSuccessfully;
                    return RedirectToAction("Login");
                case RegisterResult.MobileDuplicated:
                    TempData[ErrorMessage] = ErrorMessages.PhoneNumberExisted;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Log In
        [HttpGet(template: "/LogIn")]
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", "UserManagement");
            return View();
        }
        [HttpPost(template: "/LogIn")]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await accountService.loginAsync(model);
            //TODO Adding isbanned and notactive cases
            switch (result)
            {
                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotExisted;
                    break;
                case LoginResult.Success:
                    User? user = await accountService.GetUserByMobileAsync(model.PhoneNumber);
                    if (user == null)
                    {
                        TempData[ErrorMessage] = ErrorMessages.UserNotExisted;
                        break;
                    }
                    #region Adding Claims
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.MobilePhone,model.PhoneNumber),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                    };
                    ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimPrinciple = new ClaimsPrincipal(claimIdentity);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(claimPrinciple, properties);
                    #endregion
                    string fullname = user.GetUserName();
                    string message;
                    if (string.IsNullOrEmpty(fullname.Trim(' ')))
                    {
                        message= SuccessMessages.SignInDoneUnkownuUserSuccessfully;
                    }
                    else
                    {
                        message = fullname + SuccessMessages.SignInDoneSuccessfully;
                    }
                    TempData[SuccessMessage]=message;
                    return Redirect("/");
                case LoginResult.UserIsBaned:
                    TempData[ErrorMessage] = ErrorMessages.UserIsBanned;
                    break;
                case LoginResult.UserIsNotActive:
                    TempData[ErrorMessage] = ErrorMessages.UserNotActive;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Forgot Password
        [HttpGet(template: "/Forgot-Password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost(template: "/Forgot-Password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            var result=await accountService.ForgotPasswordAsync(model);
            switch (result)
            {
                case ForgotPasswordResult.Success:
                    TempData[SuccessMessage]=SuccessMessages.ForgotPasswordSentSuccessfully;
                    return RedirectToAction(nameof(ResetPassword));
                    case ForgotPasswordResult.Error:
                    TempData[ErrorMessage]=ErrorMessages.ErrorOccuredInSms;
                    break;
                case ForgotPasswordResult.MobileNotfound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotExisted;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Reset Password
        [HttpGet(template:"/Reset-Password")]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost(template: "/Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result=await accountService.ResetPasswordAsync(model);
            switch (result)
            {
                case ResetPasswordResult.Success:
                    TempData[SuccessMessage]=SuccessMessages.ResetPasswordDoneSuccessfully;
                    return RedirectToAction(nameof(LogIn));
                    case ResetPasswordResult.WrongCode:
                    TempData[ErrorMessage]=ErrorMessages.WrongCodeEntered;
                    break;
            }
            return View(model);
        }
        #endregion

        #region LogOut 
        [HttpGet(template:"/LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        #endregion

        #endregion

    }


}

