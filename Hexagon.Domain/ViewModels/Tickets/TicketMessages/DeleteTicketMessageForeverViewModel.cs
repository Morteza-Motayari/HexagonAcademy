using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Tickets.TicketMessages
{
    public enum DeleteTicketMessageForeverResult
    {
        Success,
        CantDeletedNow,
        FirstDeleteSimple,
        TicketMessageNotFound
    }
}
