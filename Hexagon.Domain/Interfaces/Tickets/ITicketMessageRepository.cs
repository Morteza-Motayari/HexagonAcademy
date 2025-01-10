using Hexagon.Domain.Models.Tickets;
using Hexagon.Domain.ViewModels.Tickets.TicketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Tickets
{
    public interface ITicketMessageRepository:IGenericRepository<TicketMessage>
    {
        Task<List<ClientSideTicketMessageViewModel>> GetClientSideTicketMessages(int ticketId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<List<AdminSideTicketMessageViewModel>> GetTicketMessages(int ticketId);
    }
}
