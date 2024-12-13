using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Contact_Us
{
    public class CreateContactUsViewModel
    {
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]        
        public string Subject { get; set; }
        [Display(Name = "نام و نام خانوداگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(70, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"\D+", ErrorMessage = "نام وارد شده نمی تواند شامل اعداد باشد .")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(80, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نادرست می باشد.")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(15, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string? Phone { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(800, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Description { get; set; }
        public string? IP { get; set; }
    }
    public enum CreateContactUsResult
    {
        Success
    }
}
