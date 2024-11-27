using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Gyms
{
    public class AdminSideDetailGymViewModel: BaseAdminDetail
    {
        public int Id { get; set; }
        [Display(Name = "اسم باشگاه")]
        public string Name { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "مساحت(متر مربع(")]
        public int? Area { get; set; }
        [Display(Name = "شماره تلفن ثابت")]
        public string? ConstantPhone { get; set; }
        [Display(Name = "عکس")]
        public string? ImageUrl { get; set; }

    }
}
