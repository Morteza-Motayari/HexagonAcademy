using Hexagon.Application.Services.Implementation.Wallets;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Services.Interfaces.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Components
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
