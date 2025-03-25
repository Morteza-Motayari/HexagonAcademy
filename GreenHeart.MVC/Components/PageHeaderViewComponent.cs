using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Application.Services.Interfaces.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Components
{
    public class PageHeaderViewComponent(IUserService userService,IWalletService walletService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user = await userService.GetUserClientSideAsync(userId);
            ViewData["Budget"] = await walletService.GetBudgetsAsync(userId);
            return View("PageHeader", user);
        }
    }
}
