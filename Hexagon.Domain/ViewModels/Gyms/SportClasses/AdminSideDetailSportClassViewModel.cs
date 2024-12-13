using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.SportClasses
{
    public class AdminSideDetailSportClassViewModel:BaseAdminDetail
    {
        [Display(Name = "اسم کلاس ورزشی")]
        public string Title { get; set; }
        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }
        [Display(Name = "از ساعت")]
        public TimeOnly StartTime { get; set; }
        [Display(Name = "تا ساعت")]
        public TimeOnly EndTime { get; set; }
        [Display(Name = "هزینه ثبت نام")]
        public double SubscriptionFee { get; set; }
        [Display(Name = " رشته ورزشی")]
        public string? sport { get; set; }
        public int SportId { get; set; }
        [Display(Name = "باشگاه")]
        public string? gym { get; set; }
        public int GymId { get; set; }
        [Display(Name = "مربی")]
        public string? trainer { get; set; }
        public int TrainerId { get; set; }
        [Display(Name = "عکس")]
        public string? ImageUrl { get; set; }
        [Display(Name = "حداکثر مقدار شاگردها")]
        public int MaxSubscription { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender Gender { get; set; }
        [Display(Name = "وضعیت کلاس")]
        public SportClassStatus ClassStatus { get; set; }
        [Display(Name = "تعداد ثبت نام شدگان")]
        public int RegisteredAthletes {  get; set; }
    }
}
