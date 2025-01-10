using Hexagon.Domain.Enums.Tickets;
using Hexagon.Domain.Models.Tickets;
using Hexagon.Domain.ViewModels.Tickets.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Tickets
{
    public interface ITicketRepository:IGenericRepository<Ticket>
    {
        Task<DateTime> GetLastModifiedDate(int id);
        Task<FilterClientSideTicketViewModel> FilterClientSideTicket(FilterClientSideTicketViewModel filter, int userId);
        Task<FilterTicketViewModel> FilterTickets(FilterTicketViewModel filter);
        Task<string> GetTicketTitle(int id);
        Task<int> GetTicketCreatorId(int TicketId);
        Task<bool> GetTicketExisting(int ticketId);
        Task<TicketStatus> GetTicketStatus(int ticketId);
    }
}
