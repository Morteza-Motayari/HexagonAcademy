using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Models.Gyms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassComments
{
    public class UserSideCommentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name = "کلاس")]
        public string ClassName { get; set; }
        public string? ClassImg { get; set; }
        public string ClassSlug { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        [Display(Name = "وضعیت")]
        public ClassCommentPending CommentStatus { get; set; }
    }
}
