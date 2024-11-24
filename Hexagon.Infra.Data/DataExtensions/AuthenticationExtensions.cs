using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.DataExtensions
{
    public static class AuthenticationExtensions
    {
        public static bool CheckAuthentication(this IHttpContextAccessor? accessor)
        {
            return accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
