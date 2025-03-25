using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Users.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.Gyms
{
    public class FilterGymViewModel: BasePaging<GymViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نقش")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
    }
}
