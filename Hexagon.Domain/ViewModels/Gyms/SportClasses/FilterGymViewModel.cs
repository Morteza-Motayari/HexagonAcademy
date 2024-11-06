using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using System.ComponentModel.DataAnnotations;

namespace Hexagon.Domain.ViewModels.Gyms.SportClasses
{
    public class FilterSportClassViewModel: BasePaging<SportClassViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نقش")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public ExistingStatus? Status { get; set; }
    }
}
