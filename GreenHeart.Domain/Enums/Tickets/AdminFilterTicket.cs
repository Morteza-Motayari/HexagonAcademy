using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Enums.Tickets
{
    public enum FilterTicketStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "منتظر بررسی")]
        Pending,
        [Display(Name = "پاسخ داده شده توسط کاربر")]
        UserAnswered,
        [Display(Name = "پاسخ داده شده توسط ادمین")]
        AdminAnswered,
        [Display(Name = "بسته شده")]
        Close
    }
    public enum FilterTicketPriority
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "مهم")]
        Important,
        [Display(Name = "متوسط")]
        Medium,
        [Display(Name = "کم")]
        Low
    }
    public enum FilterTicketSection
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فنی")]
        Technical,
        [Display(Name = "مالی")]
        Financial,
        [Display(Name = "منابع انسانی")]
        HR
    }
}
