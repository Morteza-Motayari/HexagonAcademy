using Hexagon.Domain.Interfaces.Tickets;
using Hexagon.Domain.Models.Tickets;
using Hexagon.Domain.ViewModels.Tickets.TicketMessages;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Tickets
{
    public class TicketMessageRepository:GenericRepository<TicketMessage>,ITicketMessageRepository
    {
        private readonly HexagonContext _db;

        public TicketMessageRepository(HexagonContext db):base(db) 
        {
            _db = db;
        }

        public async Task<List<ClientSideTicketMessageViewModel>> GetClientSideTicketMessages(int ticketId)
        => await _db.TicketMessages.Where(tm => tm.TicketId == ticketId && !tm.IsDeleted).Select(t => new ClientSideTicketMessageViewModel
        {
            TicketMessageId = t.Id,
            CreatedDate = DateTime.Now,
            Message = t.Message,
            SenderId = (int)t.CreatedBy
        }).ToListAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.TicketMessages.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<List<AdminSideTicketMessageViewModel>> GetTicketMessages(int ticketId)
        => await _db.TicketMessages.Where(tm => tm.TicketId == ticketId).Select(t => new AdminSideTicketMessageViewModel
        {
            TicketMessageId = t.Id,
            CreatedDate = DateTime.Now,
            Message = t.Message,
            SenderId = (int)t.CreatedBy,
            SenderName=_db.Users.Where(u=>u.Id==t.CreatedBy).Select(u=>u.FirstName+" "+u.LastName).First(),
            SenderAvatar= _db.Users.Where(u => u.Id == t.CreatedBy).Select(u => u.Avatar).First(),
            IsDeleted=t.IsDeleted
        }).ToListAsync();
    }
}
