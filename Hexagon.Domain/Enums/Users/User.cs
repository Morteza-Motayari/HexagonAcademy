using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.Users
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
        Active,
        NotActive,
        Ban
    }
    public enum UserSituation
    {
        [Display(Name ="کادر")]
        Cadre,
        [Display(Name = "مربی")]
        Trainer,
        [Display(Name = "ورزشکار")]
        Athlete
    }
}
