using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Components
{
    public class UserManagementSideBarViewComponent(IUserService userService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user=await userService.GetUserClientSideAsync(userId);
            return View("UserManagementSideBar", user);
        }
    }
}
