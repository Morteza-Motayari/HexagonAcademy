using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Contact_Us
{
    public class AnswerContactUsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(800, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Answer { get; set; }
        [Display(Name = "نام")]
        public string? FullName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAnswered { get; set; }

    }
    public enum AnswerContactUsResult
    {
        Success,
        ContactUsNotFound,
        FailSendingEmail
    }
}
