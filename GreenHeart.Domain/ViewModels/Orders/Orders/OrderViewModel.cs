using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsFainally { get; set; }
        [Display(Name = "هزینه کل فاکتور")]
        public int TotalPrice { get; set; }
        [Display(Name ="تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
    }
}
