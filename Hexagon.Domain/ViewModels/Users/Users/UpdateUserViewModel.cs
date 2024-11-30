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
    public class UpdateUserViewModel
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
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(13, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "کد ملی وارد شده معتبر نمی باشد")]
        public string? NationalCode { get; set; }
        [Display(Name = "شهر")]
        public string? city { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress]
        public string? email { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string? BirthDay { get; set; }
        public string? Avatar { get; set; }
        [Display(Name = "عکس")]
        public IFormFile? NewImage { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        public UserGender? Gender { get; set; }
        [Display(Name = "وضعیت")]
        public UserStatus Status { get; set; }
        public List<int>? RolesId { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum UpdateUserResult
    {
        Success,
        MobileDuplicated,
        UserNotFound,
        InvalidDateTime
    }
}
