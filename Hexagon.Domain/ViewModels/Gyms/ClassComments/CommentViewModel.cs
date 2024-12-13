using Hexagon.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.ClassComments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name = "کاربر")]
        public string? UserName { get; set; }
        [Display(Name = "کلاس")]
        public string SportClass { get; set; }
        public int? UserId { get; set; }
        public int? ClassId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "وضعیت")]
        public ClassCommentPending CommentStatus { get; set; }
    }
}
