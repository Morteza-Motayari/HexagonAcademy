using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Implementation.Wallets;
using GreenHeart.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Components
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
