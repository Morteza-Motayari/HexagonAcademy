using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Sports
{
    public class AdminSideDetailSportViewModel:BaseAdminDetail
    {
        [Display(Name = "نام رشته ورزشی")]
        public string Title { get; set; }
        [Display(Name = "مدرک مورد نیاز")]
        public string? Certificate { get; set; }
        public int CertificateId { get; set; }
    }
}
