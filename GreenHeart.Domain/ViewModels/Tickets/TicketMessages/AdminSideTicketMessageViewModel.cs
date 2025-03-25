using GreenHeart.Domain.Enums.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Tickets.TicketMessages
{
    public class AdminSideTicketMessageViewModel
    {
        public int TicketMessageId { get; set; }
        [Display(Name = "پیام")]
        public string Message { get; set; }
        [Display(Name = "پاسخ دهنده")]
        public string SenderName { get; set; }
        public string? SenderAvatar { get; set; }
        public int SenderId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
