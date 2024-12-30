using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Display(Name = "شماره تلفن همراه")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]

        public string PhoneNumber { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Password { get; set; }
        [Display(Name = "کد اعتبار سنجی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }
    public enum LoginResult
    {
        Success,
        UserNotFound,
        UserIsBaned,
        UserIsNotActive,
        InValidCaptcha
    }
}
