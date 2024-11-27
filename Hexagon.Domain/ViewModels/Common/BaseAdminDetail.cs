using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Common
{
    public abstract class BaseAdminDetail
    {
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "آخرین تاریخ ویرایش شده")]
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedById { get; set; }
        public int? LastModifiedById { get; set; }
        [Display(Name = "ساخته شده توسط")]
        public string? CreatedBy { get; set; }
        [Display(Name = "آخرین ویرایش شده توسط")]
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
