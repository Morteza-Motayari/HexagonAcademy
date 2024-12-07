using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hexagon.MVC.Utilities
{
    public class AuthenticateUserForAdminAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var staffService = context.HttpContext.RequestServices
                .GetRequiredService<IStaffService>();
            int CurrentUserId=context.HttpContext.User.GetUserId();
            if(!await staffService.UserHasPermission(CurrentUserId))
            {
                context.Result = new RedirectResult("/UserManagement/Home/Index");
            }
            await Task.CompletedTask;
        }
    }
}
