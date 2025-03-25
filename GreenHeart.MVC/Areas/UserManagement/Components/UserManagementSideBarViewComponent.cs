using GreenHeart.Application.Services.Implementation.Wallets;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Application.Services.Interfaces.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.UserManagement.Components
{
    public class UserManagementSideBarViewComponent(IUserService userService
        ,IWalletService walletService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user=await userService.GetUserClientSideAsync(userId);
            ViewData["Budget"] = await walletService.GetBudgetsAsync(userId);
            return View("UserManagementSideBar", user);
        }
    }
}
