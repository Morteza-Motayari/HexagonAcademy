using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.RecordCategories
{
    public class AdminSideDetailRecordCategoryViewModel:BaseAdminDetail
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        public int StaffId { get; set; }
        [Display(Name = "مربی")]
        public string StaffName { get; set; }
        public ICollection<ExperienceViewModel> Experiences { get; set; }
    }
}
