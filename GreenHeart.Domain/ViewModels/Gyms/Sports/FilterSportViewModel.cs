using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.Sports
{
    public class FilterSportViewModel:BasePaging<SportViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "رشته ورزشی")]
        public string? Title { get; set; }
        [Display(Name = "مدرک")]
        public string? Certificate { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
    }
}
