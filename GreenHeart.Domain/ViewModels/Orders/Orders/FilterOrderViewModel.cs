using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class FilterOrderViewModel:BasePaging<OrderViewModel>
    {
        [Display(Name = "تعداد مدل های نمایشی در صفحه")]
        public override int TakeEntity { get; set; }
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }
        [Display(Name = "وضعیت فاکتور")]
        public FilterOrderStatus OrderStatus { get; set; }
    }
}
