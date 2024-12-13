using Hexagon.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.KeyWords
{
    public class AdminSideDetailKeyWordViewModel : BaseAdminDetail
    {
        [Display(Name = "عبارت")]
        public string Key { get; set; }
        [Display(Name = "کلاس ورزشی")]
        public string? SportClass { get; set; }
        public int? SportClassId { get; set; }
    }
}
