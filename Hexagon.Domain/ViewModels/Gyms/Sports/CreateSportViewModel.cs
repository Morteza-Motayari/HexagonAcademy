using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Sports
{
    public class CreateSportViewModel
    {
        [Display(Name = "نام رشته ورزشی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        [Display(Name = "مدرک مورد نیاز")]
        public int CertificateId { get; set; }
    }
    public enum CreateSportResult
    {
        Success,
        DuplicatedTitle
    }
}
