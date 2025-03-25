using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Staffs.Trainers
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "دریافتی")]
        public int Salary { get; set; }
        [Display(Name = "عنوان")]
        public string Position { get; set; }
        [Display(Name = "کاربر")]
        public int UserId { get; set; }
        [Display(Name = "جنسیت")]
        public UserGender? Gender { get; set; }
        public ICollection<Certificate>? TrainerCertificates { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsDeleted { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }        
    }
}
