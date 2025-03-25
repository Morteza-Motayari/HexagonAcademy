using GreenHeart.Domain.Enums.Wallets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Wallets
{
    public class AdminChargeWalletViewModel
    {
        [Display(Name ="کاربر")]
        public int UserId { get; set; }
        [Display(Name = "قیمت")]
        public double Price { get; set; }
        [Display(Name = "نوع پرداخت")]
        public TransactionType Type { get; set; }
        [Display(Name = "علت پرداخت")]
        public TransactionCase Case { get; set; }
        [Display(Name = "ای پی")]
        public string? IP { get; set; }
        [Display(Name = "سیستم عامل")]
        public string? OS { get; set; }
        [Display(Name = "کدپیگیری")]
        public string RefId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }
    public enum AdminChargeWalletResult
    {
        Success
    }
}
