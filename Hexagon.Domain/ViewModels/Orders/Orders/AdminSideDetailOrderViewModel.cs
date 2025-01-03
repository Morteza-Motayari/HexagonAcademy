using Hexagon.Domain.Models.Orders;
using Hexagon.Domain.ViewModels.Common;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using Hexagon.Domain.ViewModels.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Orders.Orders
{
    public class AdminSideDetailOrderViewModel:BaseAdminDetail
    {
        [Display(Name = "وضعیت پرداخت")]
        public bool IsFainally { get; set; }
        [Display(Name = "هزینه کل فاکتور")]
        public int TotalPrice { get; set; }
        public ICollection<ClassOrderDetailViewModel>? ClassesOrder { get; set; }
        public ICollection<OrderWalletViewModel>? Wallets { get; set; }
    }
}
