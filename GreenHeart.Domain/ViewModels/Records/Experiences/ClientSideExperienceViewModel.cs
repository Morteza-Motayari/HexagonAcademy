using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Records.Experiences
{
    public class ClientSideExperienceViewModel
    {
        [Display(Name = "سابقه")]
        public string Title { get; set; }
        [Display(Name = "شرکت")]
        public string? Company { get; set; }
        [Display(Name = "توضیحات")]
        public string? Detail { get; set; }
        [Display(Name = "مدت زمان سابقه")]
        public string HowLong { get; set; }
        [Display(Name = "مدرک")]
        public string? Certificate { get; set; }
    }
}
