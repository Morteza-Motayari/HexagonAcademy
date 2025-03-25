using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class CreateSportClassViewModel
    {
        [Display(Name = "اسم کلاس ورزشی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string StartDate { get; set; }
        [Display(Name = "از ساعت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TimeOnly StartTime { get; set; }
        [Display(Name = "تا ساعت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TimeOnly EndTime { get; set; }
        [Display(Name = "هزینه ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public double SubscriptionFee { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        [Display(Name = " رشته ورزشی")]
        public int SportId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        [Display(Name = "باشگاه")]
        public int GymId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مربی")]
        public int TrainerId { get; set; }
        [Display(Name = "عکس")]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "حداکثر مقدار شاگردها")]
        public int MaxSubscription { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        [Display(Name = "جنسیت")]
        public UserGender Gender { get; set; }
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        [Display(Name = "وضعیت کلاس")]
        public SportClassStatus ClassStatus { get; set; }
    }
    public enum CreateSportClassResult
    {
        Success,
        InvalidDateTime,
        InvalidEndTime
    }
}
