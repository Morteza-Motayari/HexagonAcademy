using Hexagon.Domain.Enums.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Users
{
    public class ClientSideUpdateUserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string LastName { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "کد ملی وارد شده معتبر نمی باشد")]
        public string? NationalCode { get; set; }
        [Display(Name = "شهر")]
        public string? city { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [EmailAddress(ErrorMessage ="آدرس ایمیل وارد شده معتبر نمی باشد.")]
        public string email { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string? BirthDay { get; set; }
        public string? Avatar { get; set; }
        [Display(Name = "عکس")]
        public IFormFile? NewImage { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت خود را تعیین کنید.")]
        public UserGender? Gender { get; set; }
    }
    public enum ClientSideUpdateUserResult
    {
        Success,
        UserNotFound,
        InvalidDateTime
    }
}
