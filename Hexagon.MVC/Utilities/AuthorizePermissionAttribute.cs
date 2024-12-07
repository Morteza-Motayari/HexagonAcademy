using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Policy;

namespace Hexagon.MVC.Utilities
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
