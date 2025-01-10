using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Tickets.TicketMessages
{
    public class AdminSideAnswerTicketMessageViewModel
    {
        public int TicketId { get; set; }
        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(2000, ErrorMessage = "تعداد کارکتر وارد شده بیش از حد مجاز است.")]
        public string AnswerTicket { get; set; }
    }
    public enum AdminSideAnswerTicketMessageResult
    {
        Success,
        TicketNotFound,
        TicketClosed
    }
}
