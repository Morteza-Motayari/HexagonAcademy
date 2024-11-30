using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Trainers
{
    public class CreateTrainerViewModel
    {
        [Display(Name = "دریافتی")]
        public string? Salary { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Position { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید.")]
        public int UserId { get; set; }
        [Display(Name = "مدرک")]
        [Required(ErrorMessage = "لطفا حداقل یک {0} را انتخاب کنید.")]
        public List<int> TrainerCertificatesIds { get; set; }
    }
    public enum CreateTrainerResult
    {
        Success,
        DuplicatedPosition,
        UserNotFound,
        InValidSalary,
        ExistCertificateForUser
    }
}
