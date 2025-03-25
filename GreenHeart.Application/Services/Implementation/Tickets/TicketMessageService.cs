using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Tickets;
using GreenHeart.Domain.Enums.Tickets;
using GreenHeart.Domain.Interfaces.Tickets;
using GreenHeart.Domain.Models.Tickets;
using GreenHeart.Domain.ViewModels.Tickets.TicketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Tickets
{
    public class TicketMessageService(ITicketMessageRepository ticketMessageRepository
        ,ITicketRepository ticketRepository) : ITicketMessageService
    {
        public async Task<AdminSideAnswerTicketMessageResult> AdminSideAnswerTicketMessageAsync(AdminSideAnswerTicketMessageViewModel model)
        {
            var ticket=await ticketRepository.GetByIdAsync(model.TicketId);
            if (ticket == null)
                return AdminSideAnswerTicketMessageResult.TicketNotFound;

            if(ticket.Status==TicketStatus.Close)
                return AdminSideAnswerTicketMessageResult.TicketClosed;

            TicketMessage ticketAnswer = new()
            {
                TicketId = model.TicketId,
                Message = model.AnswerTicket
            };
            await ticketMessageRepository.InserAsync(ticketAnswer);
            await ticketMessageRepository.SaveChangeAsync();
            
            ticket.Status = TicketStatus.UserAnswered;
            ticketRepository.Update(ticket);
            await ticketRepository.SaveChangeAsync();

            return AdminSideAnswerTicketMessageResult.Success;
        }

        public async Task<AdminSideDeleteTicketMessageResult> AdminSideDeleteTicketMessageAsync(int ticketMessageId)
        {
            var ticketMessage=await ticketMessageRepository.GetByIdAsync(ticketMessageId);
            if (ticketMessage == null) 
                return AdminSideDeleteTicketMessageResult.TicketMessageNotFound;
            if(ticketMessage.IsDeleted==true)
                return AdminSideDeleteTicketMessageResult.TicketMessageAlreadyDeleted;

            ticketMessage.IsDeleted = true;
            ticketMessageRepository.Update(ticketMessage);
            await ticketMessageRepository.SaveChangeAsync();
            return AdminSideDeleteTicketMessageResult.Success;
        }

        public async Task<string> CantDeleteTicketMessageForeverNowMessage(int TicketMessageId)
        {
            DateTime lastEdit = await ticketMessageRepository.GetLastModifiedDate(TicketMessageId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این پیام تیکت تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<ClientSideDeleteTicketMessageResult> ClientSideDeleteTicketMessageAsync(int ticketMessageId)
        {
            var ticketmessage = await ticketMessageRepository.GetByIdAsync(ticketMessageId);
            if (ticketmessage == null)
                return ClientSideDeleteTicketMessageResult.TicketMessageNotFound;
            if(ticketmessage.CreatedDate.OnehourePasse())
                return ClientSideDeleteTicketMessageResult.TimePassed;

            ticketMessageRepository.Delete(ticketmessage);
            await ticketMessageRepository.SaveChangeAsync();
            return ClientSideDeleteTicketMessageResult.Success;

        }

        public async Task<List<ClientSideTicketMessageViewModel>> ClientSideGetTicketMessagesAsync(int ticketId)
        => await ticketMessageRepository.GetClientSideTicketMessages(ticketId);

        public async Task<ClientSideResponseTicketMessageResult> ClientSideResponseTickeMessageAsync(ClientSideResponseTicketMessageViewModel model)
        {
            var ticket = await ticketRepository.GetByIdAsync(model.TicketId);
            if (ticket == null)
                return ClientSideResponseTicketMessageResult.TicketNotFound;

            if(ticket.Status==TicketStatus.Close)
                return ClientSideResponseTicketMessageResult.TicketClosed;

            TicketMessage TicketReposnse = new()
            {
                Message = model.ResponseTicket,
                TicketId = model.TicketId
            };
            await ticketMessageRepository.InserAsync(TicketReposnse);
            await ticketMessageRepository.SaveChangeAsync();
            return ClientSideResponseTicketMessageResult.Success;
        }

        public async Task<ClientSideCreateTicketMessageResult> CreateTicketMessageAsync(ClientSideCreateTicketMessageViewModel model)
        {
            Ticket ticket = new()
            {
                Title = model.Title,
                Status = TicketStatus.Pending,
                Section = model.Section,
                Priority = model.Priority
            };
            await ticketRepository.InserAsync(ticket);
            await ticketRepository.SaveChangeAsync();

            TicketMessage ticketMessage = new()
            {
                Message = model.Message,
                TicketId = ticket.Id,
            };
            await ticketMessageRepository.InserAsync(ticketMessage);
            await ticketMessageRepository.SaveChangeAsync();

            return ClientSideCreateTicketMessageResult.Success;
        }

        public async Task<DeleteTicketMessageForeverResult> DeleteTicketMessageForeverAsync(int ticketMessageId)
        {
            DateTime LastDate=await ticketMessageRepository.GetLastModifiedDate(ticketMessageId);
            if (LastDate.SixMonthPassed())
            {
                var ticketMessage = await ticketMessageRepository.GetByIdAsync(ticketMessageId);
                if (ticketMessage == null)
                    return DeleteTicketMessageForeverResult.TicketMessageNotFound;

                if (ticketMessage.IsDeleted == false)
                    return DeleteTicketMessageForeverResult.FirstDeleteSimple;
                ticketMessageRepository.Delete(ticketMessage);
                await ticketMessageRepository.SaveChangeAsync();

                return DeleteTicketMessageForeverResult.Success;
            }
            else
            {
                return DeleteTicketMessageForeverResult.CantDeletedNow;
            }
        }

        public async Task<List<AdminSideTicketMessageViewModel>> GetTicketMessagesAsync(int ticketId)
        => await ticketMessageRepository.GetTicketMessages(ticketId);
    }
}
