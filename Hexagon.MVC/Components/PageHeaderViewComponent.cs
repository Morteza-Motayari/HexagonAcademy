using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Services.Interfaces.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
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
