using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using System.ComponentModel.DataAnnotations;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class FilterSportClassViewModel: BasePaging<SportClassViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نام کلاس")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
        [Display(Name = "رشته ورزشی")]
        public int? SportId { get; set; }
        [Display(Name = "باشگاه")]
        public int? GymId { get; set; }
        [Display(Name = "جنسیت")]
        public FilterUserGender? Gender { get; set; }
        [Display(Name = "وضعیت کلاس")]
        public FilterSportClassStatus? ClassStatus { get; set; }
    }
}
