using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Extensions
{
    public static class DateComparison
    {
        public static bool OneMonthPassed(this DateTime date)
        {
            TimeSpan passedTime=DateTime.Now-date;
            if (passedTime.TotalDays > 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
