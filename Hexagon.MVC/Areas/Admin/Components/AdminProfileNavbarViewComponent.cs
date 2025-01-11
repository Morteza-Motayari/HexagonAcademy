using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Implementation.Wallets;
using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Components
{
    public class AdminProfileNavbarViewComponent(IUserService userService): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user = await userService.GetUserClientSideAsync(userId);
            return View("AdminProfileNavbar", user);
        }
    }
}
