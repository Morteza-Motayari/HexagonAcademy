using GreenHeart.Domain.Enums.Tickets;
using GreenHeart.Domain.ViewModels.Tickets.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Tickets
{
    public interface ITicketService
    {
        Task<FilterTicketViewModel> FilterTicketAsync(FilterTicketViewModel filter);
        Task<FilterClientSideTicketViewModel> ClientSideFilterTicketViewModelAsync(FilterClientSideTicketViewModel filter,int userId);
        Task<DeleteTicketResult> DeleteTicketAsync(int ticketId);
        Task<DeleteTicketForeverResult> DeleteTicketForeverAsync(int ticketId);
        Task<UpdateTicketStatusResult> UpdateTicketStatusAsync(UpdateTicketStatusViewModel model);
        Task<string> CantDeleteTicketForeverNowMessage(int TicketId);
        Task<string> GetTicketTitleAsync(int ticketId);
        Task<int> GetTicketCreatorIdAsync(int ticketId);
        Task<bool>GetTicketExistingAsync(int ticketId);
        Task<TicketStatus>GetTicketStatusAsync(int ticketId);
        Task<TicketViewModel?> GetTicketViewModelAsync(int id);

    }
}
