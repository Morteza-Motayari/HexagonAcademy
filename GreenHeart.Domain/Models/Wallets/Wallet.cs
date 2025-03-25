using GreenHeart.Domain.Enums.Wallets;
using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Wallets
{
    public class Wallet:BaseEntity<int>
    {
        #region Properties
        public int UserId {  get; set; }
        public int? OrderId { get; set; }
        public double Price { get; set; }
        public TransactionType Type { get; set; }
        public TransactionCase Case { get; set; }
        public bool IsPayed { get; set; }
        public string IP { get; set; }
        public string OS { get; set; }
        public string? RefId { get; set; }
        public string? Description { get; set; }
        public string? Authority { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
        #endregion
    }
}
