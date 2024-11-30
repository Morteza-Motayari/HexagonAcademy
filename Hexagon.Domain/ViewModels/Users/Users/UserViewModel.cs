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
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(Name = "شهر")]
        public string? city { get; set; }
        [Display(Name = "ایمیل")]
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
