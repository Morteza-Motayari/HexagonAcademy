using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.RecordCategories
{
    public class RecordCategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        public int StaffId { get; set; }
        [Display(Name = "مربی")]
        public string StaffName { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
