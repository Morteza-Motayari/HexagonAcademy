using GreenHeart.Domain.Enums.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class ClientSideSportClassViewModel
    {
        public string slug { get; set; }
        [Display(Name = "اسم کلاس ورزشی")]
        public string Title { get; set; }
        [Display(Name = "از ساعت")]
        public TimeOnly StartTime { get; set; }
        [Display(Name = "تا ساعت")]
        public TimeOnly EndTime { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "مخاطب")]
        public UserGender Gender { get; set; }
    }
}
