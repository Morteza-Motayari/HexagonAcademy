using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Essays.EssayCategories
{
    public class EssayCategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "عنوان دسته پدر")]
        public string? EssayCategoryParent { get; set; }
        public int? EssayCategoryParentId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public bool IsDeleted { get; set; }
        [Display(Name = "آیا گروه مقاله فرزند دارد؟")]
        public bool IsHaveCategoryChild {  get; set; }
    }
}
