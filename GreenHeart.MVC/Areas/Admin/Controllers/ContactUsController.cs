using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Implementation.Users;
using GreenHeart.Application.Services.Interfaces.Contact_Us;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Contact_Us;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageContactUses")]
        public async Task<IActionResult> List(FilterContactUsViewModel filter)
        {
            var list = await contactUsService.FilterContactUsAsync(filter);
            ViewData["Title"] = Titles.AdminContactUs;
            return View(list);
        }
        #endregion

        #region Answer
        [HttpGet]
        [AuthorizePermission("AnswerContactUs")]
        public async Task<IActionResult> Answer(int id)
        {
            var contact=await contactUsService.GetContactUsForAnswer(id);
            if(contact==null)
                return NotFound();
            if(contact.IsDeleted==true)
            {
                TempData[WarningMessage]=WarningMessages.ContactUsCantbeAnsweredForDeletion;
                return RedirectToAction("List", "ContactUs", new { area = "Admin" });
            }
            if(contact.IsAnswered==true)
            {
                TempData[WarningMessage] = WarningMessages.ContactUsAlreadyAnswered;
                return RedirectToAction("List", "ContactUs", new { area = "Admin" });
            }
            return PartialView("_AnswerMessage", contact);
        }
        [HttpPost]
        public async Task<IActionResult> Answer(AnswerContactUsViewModel model)
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
            var result=await contactUsService.AnswerContactUs(model);
            switch(result)
            {
                case AnswerContactUsResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.ContactUsAnsweredSuccessfully
                    });
                case AnswerContactUsResult.ContactUsNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.ContactUsNotFound
                    });
                case AnswerContactUsResult.FailSendingEmail:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.ContactUsFailedSendingEmail
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
        [AuthorizePermission("DetailContactUs")]
        public async Task<IActionResult> Detail(int id)
        {
            var contact = await contactUsService.AdminSideDetailContactUsAsync(id);
            if (contact == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailContactUs;
            return View(contact);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteContactUs")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await contactUsService.DeleteContactUsAsync(id);
            switch (result)
            {
                case DeleteContactUsResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ContactUsDeletedSuccessfully;
                    break;
                case DeleteContactUsResult.ContactUsNotFound:
                    TempData[ErrorMessage] = ErrorMessages.ContactUsNotFound;
                    break;
                case DeleteContactUsResult.ContactUsAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.ContactUsAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteContactUsForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await contactUsService.DeleteContactUsForever(id);
            switch (result)
            {
                case DeleteForeverContactUsResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ContactUsDeletedForeverSuccessfully;
                    break;
                case DeleteForeverContactUsResult.CantDeletedNow:
                    string message = await contactUsService.CantDeleteContactUsForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverContactUsResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteContactUs;
                    break;
                case DeleteForeverContactUsResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.ContactUsNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
