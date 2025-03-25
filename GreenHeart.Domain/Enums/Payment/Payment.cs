using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Enums.Payment
{
    public enum PaymentType
    {
        [Display(Name ="کیف پول")]
        Wallet,
        [Display(Name ="درگاه پرداخت")]
        GateWay
    }
}
