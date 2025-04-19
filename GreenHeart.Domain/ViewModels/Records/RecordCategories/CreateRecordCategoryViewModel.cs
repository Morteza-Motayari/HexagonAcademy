using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.RecordCategories
{
    public class CreateRecordCategoryViewModel
    {
        [Display(Name = "عنوان")] 
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(300, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }
        public int StaffId { get; set; }
    }
    public enum CreateRecordCategoryResult
    {
        Success,
        CategoryExisted
    }
}
