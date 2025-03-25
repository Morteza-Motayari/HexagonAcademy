using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Banners
{
    public class CreateBannerViewModel
    {
        public IFormFile? BannerImage { get; set; }
        [Display(Name = "عنوان بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string BannerName { get; set; }
    }
    public enum CreateBannerResult
    {
        Success,
        DuplicatedBannerName,
        MaximumBannerReached
    }
}
