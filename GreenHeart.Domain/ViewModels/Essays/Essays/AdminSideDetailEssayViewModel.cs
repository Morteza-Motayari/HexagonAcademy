using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenHeart.Domain.ViewModels.Essays.Essays
{
    public class AdminSideDetailEssayViewModel : BaseAdminDetail
    {
        [Display(Name = "عنوان مقاله")]
        public string Title { get; set; }
        [Display(Name = "محتوی مقاله")]
        [AllowHtml]
        public string Content { get; set; }
        [Display(Name = "خلاصه مقاله")]
        public string Excerpt { get; set; }
        [Display(Name = "عکس")]
        public string? ImageUrl { get; set; }
        public EssayCategoryViewModel EssayCategory { get; set; }
        public ICollection<KeyWord>? keyWords  { get; set; }
    }
}
