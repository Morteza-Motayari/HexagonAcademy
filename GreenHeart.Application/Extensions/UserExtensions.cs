using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserName(this User user)
        {
            return user.FirstName + " " + user.LastName;
        }
        public static string GetUserName(this UserClientSideView user)
        {
            return user.FirstName + " " + user.LastName;
        }
        public static string GetUserName(this UserViewModel user)
        {
            return user.FirstName + " " + user.LastName;
        }       
        public static string GetUserName(this UpdateUserViewModel user)
        {
            return user.FirstName + " " + user.LastName;
        }
        public static string GetUserName(this ClientSideUpdateUserViewModel user)
        {
            return user.FirstName + " " + user.LastName;
        }
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? userId=claimsPrincipal.Claims.FirstOrDefault(u=>u.Type==ClaimTypes.NameIdentifier)?.Value;
            if(!string.IsNullOrEmpty(userId))
               return int.Parse(userId);

            else return default;
        }

    }
}
