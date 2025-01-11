using Hexagon.Domain.ViewModels.Records.Experiences;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Trainers
{
    public class ClientSideTrainerDetailViewModel
    {
        public string Slug { get; set; }
        [Display(Name = "نام کامل")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string email { get; set; }
        [Display(Name = "عنوان")]
        public string Position { get; set; }
        public string Avatar { get; set; }
        [Display(Name = "سابقه ها")]
        public ICollection<ClientSideExperienceViewModel> Experiences { get; set; }

    }
}
