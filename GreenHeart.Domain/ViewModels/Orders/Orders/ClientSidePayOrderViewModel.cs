using GreenHeart.Domain.Enums.Payment;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.Orders
{
    public class ClientSidePayOrderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "هزینه کل فاکتور")]
        public int TotalPrice { get; set; }
        [Display(Name = "وضعیت پرداخت")]
        public bool IsFainally { get; set; }
        public ICollection<ClientSideClassOrderDetail>? ClassesOrder { get; set; }
        [Display(Name = "کد اعتبار سنجی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
        public PaymentType Payment { get; set; }
        public int UserId { get; set; }
        public string? IP { get; set; }
        public string? OS { get; set; }
    }
    public enum ClientSidePayOrderResult
    {
        SuccessGoToGateWay,
        SuccessFromWallet,
        InSufficintWalletMoney,
        InValidCaptcha,
        ClassSapceFilled,
        ClassCantRegistered
    }
}
