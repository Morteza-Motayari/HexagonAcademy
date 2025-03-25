using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Wallets
{
    public class ClientSideFilterWallet:BasePaging<ClientSideWalletViewModel>
    {
        [Display(Name = "نوع پرداخت")]
        public FilterTransactionType? Type { get; set; }
        [Display(Name = "علت پرداخت")]
        public FilterTransactionCase? Case { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public FilterPayementStatus? PayementStatus { get; set; }
    }
}
