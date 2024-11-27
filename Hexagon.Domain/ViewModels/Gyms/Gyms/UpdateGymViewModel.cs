using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Gyms
{
    public class UpdateGymViewModel
    {
        public int Id { get; set; }
        [Display(Name = "اسم باشگاه")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Name { get; set; }
        [Display(Name = "آدرس")]
        [MaxLength(1000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Address { get; set; }
        [Display(Name = "مساحت(متر مربع(")]
        public int? Area { get; set; }
        [Display(Name = "شماره تلفن ثابت")]
        [MaxLength(11, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "شماره تلفن ثابت وارد شده معتبر نمی باشد")]
        public string? ConstantPhone { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? NewImage { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted {  get; set; }
    }
    public enum UpdateGymResult
    {
        Success,
        DuplicatedConstantPhone,
        GymNotFound
    }
}
