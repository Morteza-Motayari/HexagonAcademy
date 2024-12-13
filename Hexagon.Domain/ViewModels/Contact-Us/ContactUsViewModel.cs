using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Contact_Us
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "موضوع")]     
        public string Subject { get; set; }
        [Display(Name = "نام و نام خانوداگی")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        public string? Phone { get; set; }
        [Display(Name = "وضعیت پاسخ")]
        public bool IsAnswered { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "تاریخ پاسخ")]
        public DateTime AnsweredDate { get; set; }
        [Display(Name = "پاسخ دهنده")]
        public string? UserAnswered { get; set; }
        public bool IsDeleted { get; set; }
    }
}
