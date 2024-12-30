using Hexagon.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Orders.ClassesOrder
{
    public class ClientSideClassOrderDetail
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassSlug { get; set; }
        public DateTime CreatedDate { get; set; }
        public SportClassStatus ClassStatus { get; set; }
    }
}
