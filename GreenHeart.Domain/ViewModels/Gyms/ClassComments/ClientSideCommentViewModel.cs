using GreenHeart.Domain.Models.Gyms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassComments
{
    public class ClientSideCommentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name = "کاربر")]
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public int? UserId { get; set; }
        public int? ClassId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public ICollection<ClassCommentReaction>? Like { get; set; }
        public ICollection<ClassCommentReaction>? DisLike { get; set; }
    }
}
