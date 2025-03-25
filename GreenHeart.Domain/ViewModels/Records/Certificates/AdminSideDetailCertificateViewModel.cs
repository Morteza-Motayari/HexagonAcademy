using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.Certificates
{
    public class AdminSideDetailCertificateViewModel:BaseAdminDetail
    {
        [Display(Name = "مدرک")]
        public string Name { get; set; }
        [Display(Name = "جزئیات")]
        public string? Details { get; set; }
    }
}
