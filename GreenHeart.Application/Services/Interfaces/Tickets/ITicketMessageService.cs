using GreenHeart.Domain.ViewModels.Tickets.TicketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Tickets
{
    public interface ITicketMessageService
    {
        Task<ClientSideCreateTicketMessageResult> CreateTicketMessageAsync(ClientSideCreateTicketMessageViewModel model);
        Task<ClientSideDeleteTicketMessageResult> ClientSideDeleteTicketMessageAsync(int ticketMessageId);
        Task<AdminSideDeleteTicketMessageResult> AdminSideDeleteTicketMessageAsync(int ticketMessageId);
        Task<DeleteTicketMessageForeverResult> DeleteTicketMessageForeverAsync(int ticketMessageId);
        Task<List<AdminSideTicketMessageViewModel>> GetTicketMessagesAsync(int ticketId);
        Task<List<ClientSideTicketMessageViewModel>> ClientSideGetTicketMessagesAsync(int ticketId);
        Task<ClientSideResponseTicketMessageResult> ClientSideResponseTickeMessageAsync(ClientSideResponseTicketMessageViewModel model);
        Task<AdminSideAnswerTicketMessageResult> AdminSideAnswerTicketMessageAsync(AdminSideAnswerTicketMessageViewModel model);
        Task<string> CantDeleteTicketMessageForeverNowMessage(int TicketMessageId);
    }
}
