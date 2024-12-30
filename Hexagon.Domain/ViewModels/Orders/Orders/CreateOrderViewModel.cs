using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Orders.ClassesOrder
{
    public class CreateOrderViewModel
    {
        public bool IsFainally { get; set; }
    }
    public enum CreateOrderResult
    {
        Success
    }
}
