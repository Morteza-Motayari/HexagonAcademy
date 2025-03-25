using GreenHeart.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class ClassOrderDetailViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedDate { get; set; }
        public SportClassStatus ClassStatus { get; set; }
    }
}
