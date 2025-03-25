using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class UserSideSportClassViewModel
    {
        public int ClassId { get; set; }
        public string slug { get; set; }
        [Display(Name = "اسم کلاس ورزشی")]
        public string Title { get; set; }
        [Display(Name = "از ساعت")]
        public TimeOnly StartTime { get; set; }
        [Display(Name = "تا ساعت")]
        public TimeOnly EndTime { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "وضعیت کلاس")]
        public SportClassStatus Status { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegistraionDate { get; set; }
        [Display(Name = "تاریخ تمدید")]
        public DateTime ExtensionDate { get; set; }
        public UserGender Gender { get; set; }
        public double SubscriptionFee { get; set; }
    }
}
