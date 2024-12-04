using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.ViewModels.Common;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using System.ComponentModel.DataAnnotations;

namespace Hexagon.Domain.ViewModels.Gyms.SportClasses
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
