using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
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
