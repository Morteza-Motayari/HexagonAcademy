using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Tickets;
using Hexagon.Domain.Interfaces.Tickets;
using Hexagon.Domain.Models.Tickets;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using Hexagon.Domain.ViewModels.Tickets.Tickets;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Tickets
{
    public class TicketRepository:GenericRepository<Ticket>,ITicketRepository
    {
        private readonly HexagonContext _db;

        public TicketRepository(HexagonContext db):base(db) 
        {
            _db = db;
        }

        public async Task<FilterClientSideTicketViewModel> FilterClientSideTicket(FilterClientSideTicketViewModel filter,int userId)
        {
            var query = _db.Tickets.Include(o => o.TicketMessages).Where(t=>!t.IsDeleted&&t.CreatedBy==userId).AsQueryable();

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ClientSideTicketViewModel
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                Section=u.Section,
                Status=u.Status,
                Priority=u.Priority,
                Title=u.Title
            }));
            return filter;
        }

        public async Task<FilterTicketViewModel> FilterTickets(FilterTicketViewModel filter)
        {
            var query = _db.Tickets.Include(t => t.TicketMessages).Include(t=>t.User).AsQueryable();

            #region Filter Search
            if (filter.CreatorId.HasValue)
            {
                query=query.Where(t=>t.CreatedBy == filter.CreatorId.Value);
            }

            switch (filter.Status)
            {
                case FilterTicketStatus.All:
                    break;
                case FilterTicketStatus.Close:
                    query = query.Where(t => t.Status == TicketStatus.Close);
                    break;
                case FilterTicketStatus.UserAnswered:
                    query = query.Where(t => t.Status == TicketStatus.UserAnswered);
                    break;
                case FilterTicketStatus.AdminAnswered:
                    query = query.Where(t => t.Status == TicketStatus.AdminAnswered);
                    break;
                case FilterTicketStatus.Pending:
                    query = query.Where(t => t.Status == TicketStatus.Pending);
                    break;
            }

            switch (filter.Section)
            {
                case FilterTicketSection.All:
                    break;
                case FilterTicketSection.Financial:
                    query = query.Where(t => t.Section == TicketSection.Financial);
                    break;
                case FilterTicketSection.HR:
                    query = query.Where(t => t.Section == TicketSection.HR);
                    break;
                case FilterTicketSection.Technical:
                    query = query.Where(t => t.Section == TicketSection.Technical);
                    break;
            }

            switch (filter.Priority)
            {
                case FilterTicketPriority.All:
                    break;
                case FilterTicketPriority.Important:
                    query = query.Where(t => t.Priority == TicketPriority.Important);
                    break;
                case FilterTicketPriority.Medium:
                    query = query.Where(t => t.Priority == TicketPriority.Medium);
                    break;
                case FilterTicketPriority.Low:
                    query = query.Where(t => t.Priority == TicketPriority.Low);
                    break;
            }

            if (filter.CreatorName != null)
            {
                string[] search = filter.CreatorName.Split(' ');
                foreach (string name in search)
                {
                    query = query.Where(r => r.User.FirstName.Contains(name) || r.User.LastName.Contains(name)).Distinct();
                }
            }

            if (filter.Title != null)
            {
                query = query.Where(t=>t.Title.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new TicketViewModel
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                Section = u.Section,
                Status = u.Status,
                Priority = u.Priority,
                Title = u.Title,
                CreatorId=(int)u.CreatedBy,
                CreatorName=_db.Users.Where(t=>t.Id==u.CreatedBy).Select(t=>t.FirstName+" "+t.LastName).First(),
                IsDeleted=u.IsDeleted
            }));
            return filter;
        }

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Tickets.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<int> GetTicketCreatorId(int TicketId)
        => await _db.Tickets.Where(t=>t.Id==TicketId).Select(t=>(int)t.CreatedBy).FirstAsync();

        public async Task<bool> GetTicketExisting(int ticketId)
        => await _db.Tickets.Where(t => t.Id == ticketId).Select(t =>t.IsDeleted).FirstAsync();

        public async Task<TicketStatus> GetTicketStatus(int ticketId)
        => await _db.Tickets.Where(t => t.Id == ticketId).Select(t => (TicketStatus)t.Status).FirstAsync();

        public async Task<string> GetTicketTitle(int id)
        => await _db.Tickets.Where(t=>t.Id==id).Select(t=>t.Title).FirstAsync();
    }
}
