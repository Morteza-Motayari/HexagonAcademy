using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Convertors
{
    public static class CalenderConvertor
    {
        public static string ToShamsi(this DateTime date) 
        {
            PersianCalendar calender = new();
            int year = calender.GetYear(date);
            int mounth = calender.GetMonth(date);
            int day = calender.GetDayOfMonth(date);
            return $"{year}/{mounth.ToString("00")}/{day.ToString("00")}";
        }
    }
}
