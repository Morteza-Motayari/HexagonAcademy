using Hexagon.Application.Extensions;
using Hexagon.Application.Senders.Interfaces;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using mpNuget;
using System.Security.Claims;

namespace Hexagon.MVC.Controllers
{
    public class AccountController(IAccountService accountService
        ,IUserService userService
        ,ISmsSender smsSender) : BaseSiteController
    {
        #region Actions

        #region Register
        [HttpGet(template: "/register")]
        public IActionResult Register()
        {           
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", "UserManagement");
            ViewData["Title"] = Titles.Register;
            return View();
        }
        [HttpPost(template: "/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.Register;
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
            ViewData["Title"] = Titles.Register;
            return View(model);
        }
        #endregion

        #region Log In
        [HttpGet(template: "/LogIn")]
        public IActionResult LogIn()
        {            
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", "UserManagement");
            ViewData["Title"] = Titles.Login;
            return View();
        }
        [HttpPost(template: "/LogIn")]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.Login;
                return View(model);
            }
            #endregion
            var result = await accountService.loginAsync(model);
            switch (result)
            {
                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotExisted;
                    break;
                case LoginResult.InValidCaptcha:
                    TempData[ErrorMessage] = ErrorMessages.InvalidCaptcha;
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
            ViewData["Title"] = Titles.Login;
            return View(model);
        }
        #endregion

        #region Forgot Password
        [HttpGet(template: "/Forgot-Password")]
        public IActionResult ForgotPassword()
        {
            ViewData["Title"] = Titles.ForgetPassword;
            return View();
        }
        [HttpPost(template: "/Forgot-Password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.ForgetPassword;
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
            ViewData["Title"] = Titles.ForgetPassword;
            return View(model);
        }
        #endregion

        #region Reset Password
        [HttpGet(template:"/Reset-Password")]
        public IActionResult ResetPassword()
        {
            ViewData["Title"] = Titles.ResetPassword;
            return View();
        }
        [HttpPost(template: "/Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.ResetPassword;
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
            ViewData["Title"] = Titles.ResetPassword;
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

        #region Check User Authentication
        [HttpGet]
        public JsonResult IsAuthenticated()
        {
            return Json(User.Identity.IsAuthenticated);
        }
        #endregion

        #region Check User Info Completed
        [HttpGet]
        public async Task<JsonResult> IsUserInfoCompleted()
        {
            return Json(await userService.IsUserInfoCompleted(User.GetUserId()));
        }
        #endregion


        [HttpGet(template: "/test")]
        public IActionResult Test()
        {
            
            ViewData["Title"] = Titles.Register;
            return View();
        }
        [HttpPost(template: "/test")]
        public IActionResult Test(string PhoneNumber)
        {
            string message = "پیامک آزمایشی";
            smsSender.SendMessage(PhoneNumber, message);
            TempData[SuccessMessage] = "عملیات انجام شد";
            ViewData["Title"] = Titles.Register;
            return View();
        }
        #endregion

    }


}

