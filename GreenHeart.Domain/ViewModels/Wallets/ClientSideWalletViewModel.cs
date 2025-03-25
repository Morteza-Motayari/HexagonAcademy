using GreenHeart.Domain.Enums.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Wallets
{
    public class ClientSideWalletViewModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [Display(Name = "قیمت")]
        public double Price { get; set; }
        [Display(Name = "نوع پرداخت")]
        public TransactionType Type { get; set; }
        [Display(Name = "علت پرداخت")]
        public TransactionCase Case { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsPayed { get; set; }
        [Display(Name = "کدپیگیری")]
        public string RefId { get; set; }
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public string? Authority { get; set; }
    }
}
