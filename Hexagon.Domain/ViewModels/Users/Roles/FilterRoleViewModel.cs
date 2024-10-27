using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Roles
{
    public class FilterRoleViewModel : BasePaging<RoleViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نقش")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت")]
        public FilterRoleStatus? Status { get; set; }
        //public int TakeEntities { get; set; }
    }
    public enum FilterRoleStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "حذف شده ها")]
        Deleted,
        [Display(Name = "ویژگی های موجود")]
        NotDeleted
    }
}
