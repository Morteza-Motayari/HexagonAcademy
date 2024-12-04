using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Extensions
{
    public static class MoneyExtension
    {
        public static string ToMoney(this int money)
        {
            return money.ToString("#,0");
        }
        public static string ToMoney(this double money)
        {
            return money.ToString("#,0");
        }
    }
}
