using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Implementation.Tickets;
using Hexagon.Application.Services.Interfaces.Tickets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Tickets.TicketMessages;
using Hexagon.Domain.ViewModels.Tickets.Tickets;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class TicketController(ITicketService ticketService
        ,ITicketMessageService ticketMessageService) : AdminSideController
    {
        #region List
        public async Task<IActionResult> List(FilterTicketViewModel filter)
        {
            var tickets = await ticketService.FilterTicketAsync(filter);
            return View(tickets);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["TicketCreatorId"]=await ticketService.GetTicketCreatorIdAsync(id);
            ViewData["TicketId"] = id;
            ViewData["IsDeleted"]=await ticketService.GetTicketExistingAsync(id);
            ViewData["Status"]=await ticketService.GetTicketStatusAsync(id);
            var tickeMessages = await ticketMessageService.GetTicketMessagesAsync(id);
            return View(tickeMessages);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var result = await ticketService.DeleteTicketAsync(id);
            switch (result)
            {
                case DeleteTicketResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketDeletedSuccessFullyDone;
                    break;
                case DeleteTicketResult.TicketNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketNotFound;
                    break;
                case DeleteTicketResult.TicketAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.TicketAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List), "Ticket", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteTicketMessage(int id)
        {
            var result=await ticketMessageService.AdminSideDeleteTicketMessageAsync(id);
            switch (result)
            {
                case AdminSideDeleteTicketMessageResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketMessageDeletedSuccessFullyDone;
                    break;
                case AdminSideDeleteTicketMessageResult.TicketMessageNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketMessageNotFound;
                    break;
                case AdminSideDeleteTicketMessageResult.TicketMessageAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.TicketMessageAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List), "Ticket", new { area = "Admin" });
        }
        #endregion

        #region Delete Forever
        public async Task<IActionResult> DeleteForeverTicket(int id)
        {
            var result = await ticketService.DeleteTicketForeverAsync(id);
            switch (result)
            {
                case DeleteTicketForeverResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketDeletedForeverSuccessFullyDone;
                    break;
                case DeleteTicketForeverResult.TicketNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketNotFound;
                    break;
                case DeleteTicketForeverResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteTicket;
                    break;
                case DeleteTicketForeverResult.CantDeletedNow:
                    string message = await ticketService.CantDeleteTicketForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
            }
            return RedirectToAction(nameof(List), "Ticket", new { area = "Admin" });
        }
        public async Task<IActionResult> DeleteForeverTicketMessage(int id)
        {
            var result = await ticketMessageService.DeleteTicketMessageForeverAsync(id);
            switch (result)
            {
                case DeleteTicketMessageForeverResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketMessageDeletedForeverSuccessFullyDone;
                    break;
                case DeleteTicketMessageForeverResult.TicketMessageNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketMessageNotFound;
                    break;
                case DeleteTicketMessageForeverResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteTicketMessage;
                    break;
                case DeleteTicketMessageForeverResult.CantDeletedNow:
                    string message = await ticketMessageService.CantDeleteTicketMessageForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
            }
            return RedirectToAction(nameof(List), "Ticket", new { area = "Admin" });
        }
        #endregion

        #region Answer
        [HttpPost]
        public async Task<IActionResult> Answer(AdminSideAnswerTicketMessageViewModel model)
        {
            var result = await ticketMessageService.AdminSideAnswerTicketMessageAsync(model);
            switch (result)
            {
                case AdminSideAnswerTicketMessageResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TicketMessageAnsweredSuccessFullyDone;
                    break;
                case AdminSideAnswerTicketMessageResult.TicketNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TicketNotFound;
                    break;
                case AdminSideAnswerTicketMessageResult.TicketClosed:
                    TempData[WarningMessage] = WarningMessages.TicketClosedCantAnswered;
                    break;
            }
            return RedirectToAction(nameof(Detail), "Ticket", new { area = "Admin", id = model.TicketId });
        }
        #endregion

        #region ChangeStatus
        public IActionResult ChangeStatus(int id) 
        {
            return PartialView("_ChangeStatus", new UpdateTicketStatusViewModel { TicketId=id});
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(UpdateTicketStatusViewModel model)
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

            var result=await ticketService.UpdateTicketStatusAsync(model);

            switch (result)
            {
                case UpdateTicketStatusResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.TicketStatusChangedSuccessFullyDone
                    });
                    case UpdateTicketStatusResult.TicketNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.TicketNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion
    }
}
