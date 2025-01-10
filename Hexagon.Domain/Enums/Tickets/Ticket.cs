using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.Tickets
{
    public enum TicketStatus
    {
        [Display(Name ="منتظر بررسی")]
        Pending,
        [Display(Name ="پاسخ داده شده توسط کاربر")]
        UserAnswered,
        [Display(Name ="پاسخ داده شده توسط ادمین")]
        AdminAnswered,
        [Display(Name ="بسته شده")]
        Close
    }
    public enum TicketPriority
    {
        [Display(Name = "مهم")]
        Important,
        [Display(Name = "متوسط")]
        Medium,
        [Display(Name ="کم")]
        Low
    }
    public enum TicketSection
    {
        [Display(Name ="فنی")]
        Technical,
        [Display(Name ="مالی")]
        Financial,
        [Display(Name ="منابع انسانی")]
        HR
    }
}
