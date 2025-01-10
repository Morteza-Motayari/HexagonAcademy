using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Tickets.TicketMessages
{
    public class ClientSideResponseTicketMessageViewModel
    {
        public int TicketId { get; set; }
        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(2000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string ResponseTicket { get; set; }
    }
    public enum ClientSideResponseTicketMessageResult
    {
        Success,
        TicketClosed,
        TicketNotFound
    }
}
