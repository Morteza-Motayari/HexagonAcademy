using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassComments
{
    public class FilterCommentViewModel:BasePaging<CommentViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "کلاس")]
        public string SportClass { get; set; }
        [Display(Name = "وضعیت")]
        public ClassCommentPendingAdmin CommentStatus { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
    }
}
