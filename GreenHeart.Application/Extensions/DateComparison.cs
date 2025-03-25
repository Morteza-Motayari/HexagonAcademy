using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Extensions
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
        public static bool SixMonthPassed(this DateTime date)
        {
            TimeSpan passedTime = DateTime.Now - date;
            if (passedTime.TotalDays > 183)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int HowManyDayLeftToDelete(this DateTime date)
        {
            TimeSpan passedTime = DateTime.Now - date;
            return 183-(int)passedTime.TotalDays;
        }

        public static bool OnehourePasse(this DateTime date)
        {
           TimeSpan passedTime= DateTime.Now - date;
            if (passedTime.TotalHours >= 1)
                return true;
            
            else
                return false;
        }
    }
}
