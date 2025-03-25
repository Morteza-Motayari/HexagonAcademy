using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Staffs.Trainers
{
    public class ClientSideTrainerForClass
    {
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        public string? TrainerSlug {  get; set; }

        [Display(Name = "عکس")]
        public string? ImageUrl { get; set; }

    }
}
