using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.UserManagement.Controllers
{
    public class SportClassController(ISportClassService sportClassService) : UserManagementBaseSideController
    {
        #region List
        public async Task<IActionResult> List(UserSideFilterSportClassViewModel filter)
        {
            var Sportclasses=await sportClassService.GetUserSportClassesAsync(User.GetUserId(), filter);
            ViewData["Title"] = Titles.UserClasses;
            return View(Sportclasses);
        }
        #endregion
    }
}
