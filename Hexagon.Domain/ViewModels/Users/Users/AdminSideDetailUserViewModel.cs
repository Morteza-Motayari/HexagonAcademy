using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Users
{
    public class AdminSideDetailUserViewModel: BaseAdminDetail
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
        public string? email { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string? BirthDay { get; set; }
        [Display(Name = "عکس")]
        public string? Avatar { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender? Gender { get; set; }
        [Display(Name = "موقعیت")]
        public UserSituation? Situation { get; set; }
        [Display(Name = "وضعیت کاربر")]
        public UserStatus? Status { get; set; }
        public List<Role>? Roles { get; set; }
        public ICollection<Staff>? Trainers { get; set; }
        public ICollection<Staff>? Caders { get; set; }
    }
}
