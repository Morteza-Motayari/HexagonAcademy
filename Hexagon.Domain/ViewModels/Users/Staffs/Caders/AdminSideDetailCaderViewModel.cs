using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Caders
{
    public class AdminSideDetailCaderViewModel:BaseAdminDetail
    {
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "دریافتی")]
        public int Salary { get; set; }
        [Display(Name = "عنوان سمت")]
        public string Position { get; set; }
        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        [Display(Name = "کد ملی")]
        public string? NationalCode { get; set; }
        [Display(Name = "شهر")]
        public string? city { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string? BirthDay { get; set; }
        [Display(Name = "عکس")]
        public string? Avatar { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender? Gender { get; set; }
        public ICollection<Role>? CaderRoles { get; set; }
    }
}
