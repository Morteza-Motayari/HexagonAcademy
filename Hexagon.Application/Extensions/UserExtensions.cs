using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserName(this User user)
        {
            return user.FirstName + " " + user.LastName;
        }
    }
}
