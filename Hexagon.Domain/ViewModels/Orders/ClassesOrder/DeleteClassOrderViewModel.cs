using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Orders.ClassesOrder
{
    public class DeleteClassOrderViewModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }
    }
    public enum DeleteClassOrderResult
    {
        Success,
        OrderNotFound
    }
}
