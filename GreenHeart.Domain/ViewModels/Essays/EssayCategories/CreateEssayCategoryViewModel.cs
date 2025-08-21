using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Essays.EssayCategories
{
    public class CreateEssayCategoryViewModel
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        public int? EssayCategoryParentId { get; set; }
    }
    public enum CreateEssayCategoryResult
    {
        Success,
        TitleDuplicated,
        EssayCategoryHasEssaysAndCantHaveChildCategory
    }
}
