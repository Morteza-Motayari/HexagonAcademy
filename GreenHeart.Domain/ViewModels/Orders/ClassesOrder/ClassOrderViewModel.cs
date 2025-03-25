using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class ClassOrderViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string Class { get; set; }
        public int Price { get; set; }
    }
}
