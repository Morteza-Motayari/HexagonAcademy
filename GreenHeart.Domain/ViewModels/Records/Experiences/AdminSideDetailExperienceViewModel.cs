using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.Experiences
{
    public class AdminSideDetailExperienceViewModel:BaseAdminDetail
    {
        public int UserId { get; set; }
        [Display(Name = "نام همکار")]
        public string UserName { get; set; }
        public int StaffId { get; set; }
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "سابقه")]
        public string Title { get; set; }
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Display(Name = "شرکت")]
        public string? Company { get; set; }
        [MaxLength(1000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Display(Name = "توضیحات")]
        public string? Detail { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مدت زمان سابقه")]
        public string HowLong { get; set; }
        [Display(Name = "مدرک")]
        public string? Certificate { get; set; }
        public int? CertificateId { get; set; }
        public int RecordCategoryId { get; set; }
        [Display(Name = "مجموعه سوابق")]
        public string RecordCategory { get; set; }
        [Display(Name = "تصویر مدرک")]
        public string ExImage { get; set; }
    }
}
