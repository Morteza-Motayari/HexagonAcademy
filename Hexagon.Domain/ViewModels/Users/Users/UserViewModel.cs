using Hexagon.Domain.Enums.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string LastName { get; set; }
        [Display(Name = "شماره موبایل")]
        [MaxLength(13, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "کلمه عبور")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Password { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "کد ملی وارد شده معتبر نمی باشد")]
        public string NationalCode { get; set; }
        public string? city { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [EmailAddress]
        public string email { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDay { get; set; }
        public string? Avatar { get; set; }
        [Display(Name = "عکس")]
        public IFormFile? Image { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender? Gender { get; set; }
        [Display(Name = "نقش")]
        public UserSituation? Situation { get; set; }
        [Display(Name ="وضعیت کاربر")]
        public UserStatus? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
