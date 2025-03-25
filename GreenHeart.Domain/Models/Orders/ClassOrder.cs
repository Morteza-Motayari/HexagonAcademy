using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Gyms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Orders
{
    public class ClassOrder:BaseEntity<int>
    {
        #region Properties
        public int OrderId { get; set; }
        public int ClassId { get; set; }
        public int Price { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(OrderId))]
        public Order order { get; set; }
        [ForeignKey(nameof(ClassId))]
        public SportClass Class { get; set; }
        #endregion
    }
}
