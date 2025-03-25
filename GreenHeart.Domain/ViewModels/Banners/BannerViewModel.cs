using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Banners
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        [Display(Name ="تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "ایجاد شده توسط")]
        public string? CreatedByName { get; set; }
        public int? CreatedBy { get; set; }
        public string BannerUrl { get; set; }
        [Display(Name = "اسم بنر")]
        public string BannerName { get; set; }
    }
}
