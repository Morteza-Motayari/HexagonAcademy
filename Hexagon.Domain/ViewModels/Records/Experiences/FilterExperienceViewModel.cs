using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Records.Experiences
{
    public class FilterExperienceViewModel:BasePaging<ExperienceViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نقش")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
    }
}
