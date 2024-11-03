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
}
