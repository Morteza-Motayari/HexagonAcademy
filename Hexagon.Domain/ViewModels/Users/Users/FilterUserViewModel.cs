using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Users
{
    public class FilterUserViewModel:BasePaging<UserViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "اسم")]
        public string? Name { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender? Gender { get; set; }
        [Display(Name = "نقش")]
        public UserSituation? Situation { get; set; }
        [Display(Name = "وضعیت کاربر")]
        public UserStatus? userStatus { get; set; }
    }
}
