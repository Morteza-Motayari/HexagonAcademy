using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.Filter
{
    public enum ExistingStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "حذف شده ها")]
        Deleted,
        [Display(Name = " موجود")]
        NotDeleted
    }
    public enum FilterUserGender
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "مذکر")]
        Male,
        [Display(Name = "مونث")]
        Female
    }
    public enum FilterUserStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "مسدود")]
        Ban
    }
    public enum FilterUserSituation
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "کادر")]
        Cadre,
        [Display(Name = "مربی")]
        Trainer,
        [Display(Name = "ورزشکار")]
        Athlete
    }
    public enum FilterSportClassStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "بسته شده")]
        Closed
    }
}
