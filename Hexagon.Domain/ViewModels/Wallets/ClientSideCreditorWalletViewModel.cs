using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Wallets
{
    public class ClientSideCreditorWalletViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "مبلغ(ریال)")]
        public double Price { get; set; }
        [Display(Name = "ای پی")]
        public string? IP { get; set; }
        [Display(Name = "سیستم عامل")]
        public string? OS { get; set; }
    }
    public enum ClientSideCreditorWalletResult
    {
        Success
    }
}
