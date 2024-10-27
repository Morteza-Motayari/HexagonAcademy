using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Extensions
{
    public static class IsEmpty
    {
        public static bool CheckNullability<T>(this ICollection<T?> list)
        {
            return list != null && list.Any();
        }
    }
}
