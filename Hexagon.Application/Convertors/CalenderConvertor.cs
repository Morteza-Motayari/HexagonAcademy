using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public static DateTime ToMiladi(this string date)
        {
            PersianCalendar p = new PersianCalendar();
            int year=int.Parse(date.Substring(0, 4));
            int month=int.Parse(date.Substring(5,2));
            int day=int.Parse(date.Substring(8,2));
            DateTime x = p.ToDateTime(year, month, day,12,0,0,0);
            return x;
        }
    }
}
