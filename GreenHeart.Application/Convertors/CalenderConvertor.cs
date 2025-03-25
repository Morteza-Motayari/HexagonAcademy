using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GreenHeart.Application.Convertors
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

        public static string ToShamsiWithHour(this DateTime date)
        {
            PersianCalendar calender = new();
            int year = calender.GetYear(date);
            int mounth = calender.GetMonth(date);
            int day = calender.GetDayOfMonth(date);
            return $"{date.Hour}:{date.Minute} {year}/{mounth.ToString("00")}/{day.ToString("00")}";
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

        public static string ToHoure(this TimeOnly time)
        {
            return time.ToString("HH:mm");
        }
        public static string ToShamsiWithPersianMonth(this DateTime date)
        {
            PersianCalendar calender = new();
            int year = calender.GetYear(date);
            int mounth = calender.GetMonth(date);
            int day = calender.GetDayOfMonth(date);
            string persianMonth = "ماه";
            switch(mounth)
            {
                case 1:
                    persianMonth = "فروردین";
                    break;
                case 2:
                    persianMonth = "اردیبهشت";
                    break;
                case 3:
                    persianMonth = "خرداد";
                    break;
                case 4:
                    persianMonth = "تیر";
                    break;
                case 5:
                    persianMonth = "مرداد";
                    break;
                case 6:
                    persianMonth = "شهریور";
                    break;
                case 7:
                    persianMonth = "مهر";
                    break;
                case 8:
                    persianMonth = "آبان";
                    break;
                case 9:
                    persianMonth = "آذر";
                    break;
                case 10:
                    persianMonth = "دی";
                    break;
                case 11:
                    persianMonth = "بهمن";
                    break;
                case 12:
                    persianMonth = "اسفند";
                    break;
            }
            return $"{date.Hour}:{date.Minute} {day.ToString("00")} {persianMonth} {year}";
        }
    }
}
