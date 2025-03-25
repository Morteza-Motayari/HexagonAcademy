using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class FilterSportClassAthleteViewModel:BasePaging<SportClassAthleteViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نام")]
        public string? AthleteName { get; set; }
        [Display(Name = "وضعیت ثبت نام")]
        public FilterRegisteredAthletesStatus? ExtensionStatus { get; set; }
        public int SportClassId { get; set; }
        public string SportClassTitle { get; set; }

    }
}
