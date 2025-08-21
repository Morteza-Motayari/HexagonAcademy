using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenHeart.Domain.ViewModels.Essays.Essays
{
    public class EssayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان مقاله")]
        public string Title { get; set; }
        [Display(Name = "محتوی مقاله")]
        [AllowHtml]
        public string Content { get; set; }
        [Display(Name = "خلاصه مقاله")]
        public string Excerpt { get; set; }
        public string EssayCategory { get; set; }
        public int EssayCagtegoryId { get; set; }
        [Display(Name = "عکس")]
        public string? ImageUrl { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public bool IsDeleted { get; set; }
        public List<int> EssayCategoryParentsId { get; set; }
    }
}
