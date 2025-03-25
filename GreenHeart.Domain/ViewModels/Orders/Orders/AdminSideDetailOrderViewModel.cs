using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Domain.ViewModels.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.Orders
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
