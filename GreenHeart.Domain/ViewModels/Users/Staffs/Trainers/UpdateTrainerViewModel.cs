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
    public class UpdateTrainerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "دریافتی")]
        public string Salary { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Position { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        public int UserId { get; set; }
        [Display(Name = "مربی")]
        public string? TrainerName { get; set; }
        public List<int>? TrainerCertificatesIds { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum UpdateTrainerResult
    {
        Success,
        DuplicatedPosition,
        TrainerNotFound,
        InValidSalary,
        ExistCertificateForUser
    }
}
