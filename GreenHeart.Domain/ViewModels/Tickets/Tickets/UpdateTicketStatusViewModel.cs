using GreenHeart.Domain.Enums.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Tickets.Tickets
{
    public class UpdateTicketStatusViewModel
    {
        public int TicketId { get; set; }
        [Display(Name ="وضعیت")]
        public TicketStatus Status { get; set; }
    }
    public enum UpdateTicketStatusResult
    {
        Success,
        TicketDeleted,
        TicketNotFound
    }
}
