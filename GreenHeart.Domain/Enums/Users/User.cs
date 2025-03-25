using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Enums.Users
{
    public enum UserGender
    {
        [Display(Name ="مذکر")]
        Male,
        [Display(Name = "مونث")]
        Female
    }
    public enum UserStatus
    {
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "مسدود")]
        Ban
    }
    public enum UserSituation
    {
        [Display(Name ="کادر")]
        Cader,
        [Display(Name = "مربی")]
        Trainer,
        [Display(Name = "ورزشکار")]
        Athlete
    }
}
