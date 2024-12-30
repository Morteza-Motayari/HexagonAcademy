using Hexagon.Domain.Models.Orders;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Orders.Orders
{
    public class ClientSideOrderDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsFainally { get; set; }
        [Display(Name = "هزینه کل فاکتور")]
        public int TotalPrice { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public ICollection<ClientSideClassOrderDetail>? ClassesOrder { get; set; }
        public DateTime PayedDate { get; set; }
    }
}
