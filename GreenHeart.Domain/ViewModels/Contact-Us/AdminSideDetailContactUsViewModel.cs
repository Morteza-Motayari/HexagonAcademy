using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Contact_Us
{
    public class AdminSideDetailContactUsViewModel:BaseAdminDetail
    {
        [Display(Name = "موضوع")]
        public string Subject { get; set; }
        [Display(Name = "نام و نام خانوداگی")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        public string? Phone { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        public string? IP { get; set; }
        [Display(Name = "وضعیت پاسخ")]
        public bool IsAnswered { get; set; }
        [Display(Name = "پاسخ")]
        public string? Answer { get; set; }
    }
}
