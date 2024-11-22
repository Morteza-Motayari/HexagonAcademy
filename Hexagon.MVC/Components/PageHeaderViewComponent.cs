using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Components
{
    public class PageHeaderViewComponent(IUserService userService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user = await userService.GetUserClientSideAsync(userId);
            return View("PageHeader", user);
        }
    }
}
