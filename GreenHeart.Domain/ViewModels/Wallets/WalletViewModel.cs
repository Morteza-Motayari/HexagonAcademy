using GreenHeart.Domain.Enums.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Wallets
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int? OrderId { get; set; }
        [Display(Name ="قیمت")]
        public double Price { get; set; }
        [Display(Name = "نوع پرداخت")]
        public TransactionType Type { get; set; }
        [Display(Name = "علت پرداخت")]
        public TransactionCase Case { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsPayed { get; set; }
        [Display(Name = "ای پی")]
        public string IP { get; set; }
        [Display(Name = "سیستم عامل")]
        public string OS { get; set; }
        [Display(Name = "کدپیگیری")]
        public string RefId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "وضعیت موجودیت")]
        public bool IsDeleted { get; set; }
        public string Authority { get; set; }
    }
}
