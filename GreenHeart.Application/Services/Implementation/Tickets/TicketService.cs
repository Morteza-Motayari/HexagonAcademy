using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Tickets;
using GreenHeart.Domain.Enums.Tickets;
using GreenHeart.Domain.Interfaces.Tickets;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Tickets;
using GreenHeart.Domain.ViewModels.Tickets.TicketMessages;
using GreenHeart.Domain.ViewModels.Tickets.Tickets;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Infra.Data.Repositories.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Tickets
{
    public class TicketService(ITicketRepository ticketRepository) : ITicketService
    {
        public async Task<string> CantDeleteTicketForeverNowMessage(int TicketId)
        {
            DateTime lastEdit = await ticketRepository.GetLastModifiedDate(TicketId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این تیکت تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<FilterClientSideTicketViewModel> ClientSideFilterTicketViewModelAsync(FilterClientSideTicketViewModel filter,int userId)
        => await ticketRepository.FilterClientSideTicket(filter, userId);

        public async Task<DeleteTicketResult> DeleteTicketAsync(int ticketId)
        {
            var ticket=await ticketRepository.GetByIdAsync(ticketId);
            if(ticket == null) 
                return DeleteTicketResult.TicketNotFound;
            if(ticket.IsDeleted==true)
                return DeleteTicketResult.TicketAlreadyDeleted;

            ticket.IsDeleted = true;
            ticketRepository.Update(ticket);
            await ticketRepository.SaveChangeAsync();
            return DeleteTicketResult.Success;
        }

        public async Task<DeleteTicketForeverResult> DeleteTicketForeverAsync(int ticketId)
        {
            DateTime LastDate = await ticketRepository.GetLastModifiedDate(ticketId);
            if (LastDate.SixMonthPassed())
            {
                var ticketMessage = await ticketRepository.GetByIdAsync(ticketId);
                if (ticketMessage == null)
                    return DeleteTicketForeverResult.TicketNotFound;

                if (ticketMessage.IsDeleted == false)
                    return DeleteTicketForeverResult.FirstDeleteSimple;
                ticketRepository.Delete(ticketMessage);
                await ticketRepository.SaveChangeAsync();

                return DeleteTicketForeverResult.Success;
            }
            else
            {
                return DeleteTicketForeverResult.CantDeletedNow;
            }
        }

        public async Task<FilterTicketViewModel> FilterTicketAsync(FilterTicketViewModel filter)
        => await ticketRepository.FilterTickets(filter);

        public async Task<int> GetTicketCreatorIdAsync(int ticketId)
        => await ticketRepository.GetTicketCreatorId(ticketId);

        public async Task<bool> GetTicketExistingAsync(int ticketId)
        => await ticketRepository.GetTicketExisting(ticketId);

        public async Task<TicketStatus> GetTicketStatusAsync(int ticketId)
        => await ticketRepository.GetTicketStatus(ticketId);

        public async Task<string> GetTicketTitleAsync(int ticketId)
        =>await ticketRepository.GetTicketTitle(ticketId);

        public async Task<TicketViewModel?> GetTicketViewModelAsync(int id)
        {
            var ticket=await ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return null;
            return new TicketViewModel
            {
                Id = ticket.Id,
                CreatedDate = ticket.CreatedDate,
                IsDeleted = ticket.IsDeleted,
                CreatorId = (int)ticket.CreatedBy,
                Status = ticket.Status,
                Title = ticket.Title
            };
        }

        public async Task<UpdateTicketStatusResult> UpdateTicketStatusAsync(UpdateTicketStatusViewModel model)
        {
            var ticket=await ticketRepository.GetByIdAsync(model.TicketId);
            if(ticket == null)
                return UpdateTicketStatusResult.TicketNotFound;

            if(ticket.IsDeleted == true)
                return UpdateTicketStatusResult.TicketDeleted;
            ticket.Status = model.Status;
            ticketRepository.Update(ticket);
            await ticketRepository.SaveChangeAsync();
            return UpdateTicketStatusResult.Success;
        }
    }
}
