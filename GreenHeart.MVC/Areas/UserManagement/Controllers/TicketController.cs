using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Tickets;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Tickets.TicketMessages;
using GreenHeart.Domain.ViewModels.Tickets.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.UserManagement.Controllers
{
    public class TicketController(ITicketService ticketService
        ,ITicketMessageService ticketMessageService,
        IUserService userService) : UserManagementBaseSideController
    {

        #region List
        public async Task<IActionResult> List(FilterClientSideTicketViewModel filter)
        {
            var tickets=await ticketService.ClientSideFilterTicketViewModelAsync(filter,User.GetUserId());
            ViewData["Title"] = Titles.UserTickets;
            return View(tickets);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_AddTicketMessage");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientSideCreateTicketMessageViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    status = 204,
                    message = ErrorMessages.InsufficintInputs
                });
            }
            #endregion

            var result =await ticketMessageService.CreateTicketMessageAsync(model);
            switch (result)
            {
                case ClientSideCreateTicketMessageResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.TicketMessageAddSuccessFullyDone
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["TicketId"] = id;
            ViewData["TicketTitle"]=await ticketService.GetTicketTitleAsync(id);
            ViewData["userAvatar"]=await userService.GetUserAvatarUrlAsync(User.GetUserId());
            ViewData["TicketStatus"]= await ticketService.GetTicketStatusAsync(id);
            var tickeMessages=await ticketMessageService.ClientSideGetTicketMessagesAsync(id);
            ViewData["Title"] = Titles.UserTicketDetail;
            return View(tickeMessages);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result=await ticketMessageService.ClientSideDeleteTicketMessageAsync(id);
            switch(result)
            {
                case ClientSideDeleteTicketMessageResult.Success:
                    TempData[SuccessMessage]=SuccessMessages.TicketMessageDeletedSuccessFullyDone;
                    break;
                case ClientSideDeleteTicketMessageResult.TicketMessageNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketMessageNotFound;
                    break;
                    case ClientSideDeleteTicketMessageResult.TimePassed:
                    TempData[ErrorMessage] = ErrorMessages.TicketMessageCantDletedForPassedTime;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Response
        [HttpPost]
        public async Task<IActionResult> Response(ClientSideResponseTicketMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ErrorMessages.TicketMessageEmpty;
                return RedirectToAction(nameof(Detail), "Ticket", new { area = "UserManagement", id = model.TicketId });
            }
            var result = await ticketMessageService.ClientSideResponseTickeMessageAsync(model);
            switch(result)
            {
                case ClientSideResponseTicketMessageResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketMessageRespondedSuccessFullyDone;
                    return RedirectToAction(nameof(List), "Ticket", new { area = "UserManagement" });
                case ClientSideResponseTicketMessageResult.TicketNotFound:
                    TempData[ErrorMessage]=ErrorMessages.TicketNotFound;
                    break;
                    case  ClientSideResponseTicketMessageResult.TicketClosed:
                    TempData[WarningMessage]=WarningMessages.TicketClosedCantResponded;
                    break;
            }
            return RedirectToAction(nameof(Detail), "Ticket", new { area = "UserManagement", id = model.TicketId });
        }
        #endregion

    }
}
