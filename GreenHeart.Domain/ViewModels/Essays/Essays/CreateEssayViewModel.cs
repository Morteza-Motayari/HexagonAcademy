using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenHeart.Domain.ViewModels.Essays.Essays
{
    public class CreateEssayViewModel
    {
        [Display(Name ="عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        [Display(Name = "محتوی مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [AllowHtml]
        public string Content { get; set; }
        [Display(Name = "خلاصه مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(500, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Excerpt { get; set; }
        [Display(Name = "عکس")]
        public IFormFile? Image { get; set; }
        public int EssayCagtegoryId { get; set; }
    }
    public enum CreateEssayResult
    {
        Success,
        TitleDuplicated,
        CantSelectParentCategory
    }
}
