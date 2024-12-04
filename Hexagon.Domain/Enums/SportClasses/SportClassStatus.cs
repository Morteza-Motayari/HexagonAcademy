using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.SportClasses
{
    public enum SportClassStatus
    {
        [Display(Name ="فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "بسته شده")]
        Closed
    }
}
