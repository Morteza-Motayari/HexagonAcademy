using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Records.Certificates
{
    public class UpdateCertificateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Display(Name = "مدرک")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(1000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        [Display(Name = "جزئیات")]
        public string? Details { get; set; }
    }
    public enum UpdateCertificateResult
    {
        Success,
        DuplicatedCertificate,
        CertificateNotFound
    }
}
