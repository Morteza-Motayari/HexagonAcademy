using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Policy;

namespace GreenHeart.MVC.Utilities
{
    public class AuthorizePermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permissionName;

        public AuthorizePermissionAttribute(string PermissionName)
        {
            _permissionName = PermissionName;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = context.HttpContext.RequestServices
                .GetRequiredService<IUserService>();
            int CurrentUserId=context.HttpContext.User.GetUserId();

            if (!await userService.CaderHasPermissionAsync(CurrentUserId, _permissionName))
            {
                context.Result = new RedirectResult("/Admin/Home/Index");
            }
           await Task.CompletedTask;
        }
    }
}
