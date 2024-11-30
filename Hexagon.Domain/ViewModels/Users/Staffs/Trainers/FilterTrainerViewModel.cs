using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Trainers
{
    public class FilterTrainerViewModel : BasePaging<TrainerViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "عنوان")]
        public string? Position { get; set; }
        [Display(Name = "اسم")]
        public string? Name { get; set; }
        [Display(Name = "جنسیت")]
        public FilterUserGender? Gender { get; set; }
        [Display(Name = "شماره موبایل")]
        [MaxLength(13, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
        [Display(Name = "مدرک")]
        public int? CertificateId { get; set; }
    }
}
