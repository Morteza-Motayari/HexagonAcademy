using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.ClassComments
{
    public class AdminSideDetailClassCommentViewModel:BaseAdminDetail
    {
        public int Id { get; set; }
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name = "کلاس ورزشی")]
        public string? SportClass { get; set; }
        public int ClassId { get; set; }
        [Display(Name = "وضعیت")]
        public ClassCommentPending CommentStatus { get; set; }
    }
}
