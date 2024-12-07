using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Caders
{
    public class FilterCaderViewModel: BasePaging<CaderViewModel>
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
        [Display(Name = "نقش")]
        public int? RoleId { get; set; }
    }
}
