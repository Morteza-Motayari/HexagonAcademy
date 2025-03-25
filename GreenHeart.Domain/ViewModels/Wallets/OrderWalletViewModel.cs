using GreenHeart.Domain.Enums.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Wallets
{
    public class OrderWalletViewModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [Display(Name = "نوع پرداخت")]
        public TransactionType Type { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsPayed { get; set; }
    }
}
