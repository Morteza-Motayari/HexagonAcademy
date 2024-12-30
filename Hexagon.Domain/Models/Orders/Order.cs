using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.Models.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Orders
{
    public class Order:BaseEntity<int>
    {
        #region Properties
        public bool IsFainally { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(CreatedBy))]
        public User User { get; set; }
        public ICollection<ClassOrder>? ClassesOrder { get; set; }
        public ICollection<Wallet>? Wallets { get; set; }
        #endregion
    }
}
