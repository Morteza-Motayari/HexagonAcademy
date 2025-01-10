using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.SportClasses
{
    public class ClientSideFilterSportClassViewModel:BasePaging<ClientSideSportClassViewModel>
    {
        [Display(Name = "نام کلاس")]
        public string? Title { get; set; }
        [Display(Name = "رشته ورزشی")]
        public int? SportId { get; set; }
        public string? SportSlug { get; set; }
        public string? KeyWord { get; set; }
        [Display(Name = "باشگاه")]
        public int? GymId { get; set; }
        [Display(Name = "جنسیت")]
        public FilterUserGender? Gender { get; set; }
    }
}
