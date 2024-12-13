using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.SportClasses
{
    public enum ClassCommentPending
    {
        [Display(Name ="منتظر تایید ادمین")]
        CommentSent,
        [Display(Name = "پذیرفته شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }
    public enum ClassCommentPendingAdmin
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "منتظر تایید ادمین")]
        CommentSent,
        [Display(Name = "پذیرفته شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }
}
