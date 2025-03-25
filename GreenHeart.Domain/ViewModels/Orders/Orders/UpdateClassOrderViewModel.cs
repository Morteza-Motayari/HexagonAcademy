using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class UpdateOrderViewModel
    {
        public int Id { get; set; }
        public bool IsFainally { get; set; }
    }
    public enum UpdateOrderResult
    {
        Success,
        OrderNotFound
    }
}
