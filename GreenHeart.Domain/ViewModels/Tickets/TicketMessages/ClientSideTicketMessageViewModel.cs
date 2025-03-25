using GreenHeart.Domain.Enums.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Tickets.TicketMessages
{
    public class ClientSideTicketMessageViewModel
    {
        public int TicketMessageId { get; set; }
        [Display(Name = "پیام")]
        public string Message { get; set; }
        public int SenderId { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; }

    }
}
