using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hexagon.MVC.Utilities.ActionFilters
{
    public class UserInfoFilter : IAsyncActionFilter
    {
        //public async void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var userService = context.HttpContext.RequestServices
        //        .GetRequiredService<IUserService>();
        //    int CurrentUserId = context.HttpContext.User.GetUserId();
        //    if (!await userService.IsUserInfoCompleted(CurrentUserId))
        //    {
        //        context.Result = new RedirectResult("/UserManagement/User/EditProfile");
        //    }
        //    await Task.CompletedTask;
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{

        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userService = context.HttpContext.RequestServices
                .GetRequiredService<IUserService>();
            int CurrentUserId = context.HttpContext.User.GetUserId();
            if (!await userService.IsUserInfoCompleted(CurrentUserId))
            {
                context.Result = new RedirectResult("/UserManagement/User/EditProfile");
            }
            else
            {
                await next();
            }
        }
    }
}
