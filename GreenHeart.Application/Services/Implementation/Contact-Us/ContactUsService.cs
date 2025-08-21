using GreenHeart.Application.Extensions;
using GreenHeart.Application.Senders.Interfaces;
using GreenHeart.Application.Services.Interfaces.Contact_Us;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Contact_Us;
using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Contact_Us;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Infra.Data.Repositories.Essays;
using GreenHeart.Infra.Data.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Contact_Us
{
    public class ContactUsService(IContactUsRepository contactUsRepository
        ,IUserRepository userRepository
        ,IEmailSender emailSender) : IContactUsService
    {
        public async Task<AdminSideDetailContactUsViewModel?> AdminSideDetailContactUsAsync(int ContactUsId)
        {
            var contact=await contactUsRepository.GetByIdAsync(ContactUsId);
            if(contact == null) 
                return null;
            return new AdminSideDetailContactUsViewModel
            {
                Id = contact.Id,
                Subject = contact.Subject,
                Answer = contact.Answer,
                FullName = contact.FullName,
                Description = contact.Description,
                IP = contact.IP,
                Email = contact.Email,
                Phone = contact.Phone,
                IsAnswered = contact.IsAnswered,
                IsDeleted = contact.IsDeleted,
                CreatedDate = contact.CreatedDate,
                LastModifiedDate = contact.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(contact.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(contact.LastModifiedBy),
                CreatedById = contact.CreatedBy,
                LastModifiedById = contact.LastModifiedBy
            };
        }

        public async Task<AnswerContactUsResult> AnswerContactUs(AnswerContactUsViewModel ContactUs)
        {
           var contact=await contactUsRepository.GetByIdAsync(ContactUs.Id);
            if (contact == null) 
                return AnswerContactUsResult.ContactUsNotFound;
            contact.Answer=ContactUs.Answer;
            contact.IsAnswered=true;

            #region Sending Email
            string body = $@"
<h3>پاسخ به پیام با عنوان {contact.Subject} به شرح زیر می باشد</h3>
<p>پاسخ:{ContactUs.Answer}</p>
";
            var sendEmailResult=await emailSender.Send(contact.Email,contact.Subject,body);
            if(sendEmailResult==false )
                return AnswerContactUsResult.FailSendingEmail;
            #endregion

            contactUsRepository.Update(contact);
            await contactUsRepository.SaveChangeAsync();
            return AnswerContactUsResult.Success;
        }

        public async Task<string> CantDeleteContactUsForeverNowMessage(int ContactUsId)
        {
            DateTime lastEdit = await contactUsRepository.GetLastModifiedDate(ContactUsId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این درخواست ارتباط تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model)
        {
            ContactUs contact = new()
            {
                Subject = model.Subject,
                FullName = model.FullName,
                Email = model.Email,
                Description = model.Description,
                IP=model.IP,
                Phone=model.Phone
            };
            await contactUsRepository.InserAsync(contact);
            await contactUsRepository.SaveChangeAsync();
            return CreateContactUsResult.Success;
        }

        public async Task<DeleteContactUsResult> DeleteContactUsAsync(int ContactUsId)
        {
            var contact=await contactUsRepository.GetByIdAsync(ContactUsId);
            if(contact == null)
                return DeleteContactUsResult.ContactUsNotFound;
            if(contact.IsDeleted==true)
                return DeleteContactUsResult.ContactUsAlreadyDeleted;
            contact.IsDeleted = true;
            contactUsRepository.Update(contact);
            await contactUsRepository.SaveChangeAsync();
            return DeleteContactUsResult.Success;
        }

        public async Task<DeleteForeverContactUsResult> DeleteContactUsForever(int ContactUsId)
        {
            var contactus = await contactUsRepository.GetByIdAsync(ContactUsId);

            if (contactus == null)
                return DeleteForeverContactUsResult.NotFound;

            if (contactus.IsDeleted == false)
                return DeleteForeverContactUsResult.FirstDeleteSimple;

            DateTime lastDate = await contactUsRepository.GetLastModifiedDate(ContactUsId);
            if (lastDate.SixMonthPassed())
            {
                contactUsRepository.Delete(contactus);
                await contactUsRepository.SaveChangeAsync();
                return DeleteForeverContactUsResult.Success;
            }
            else
            {
                return DeleteForeverContactUsResult.CantDeletedNow;
            }
        }

        public async Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter)
        => await contactUsRepository.FilterContactUsAsync(filter);

        public async Task<AnswerContactUsViewModel> GetContactUsForAnswer(int ContactUsId)
        {
            var contactus=await contactUsRepository.GetByIdAsync(ContactUsId);
            if (contactus == null)
                return null;
            return new AnswerContactUsViewModel()
            {
                Id = contactus.Id,
                FullName = contactus.FullName,
                IsAnswered = contactus.IsAnswered,
                IsDeleted = contactus.IsDeleted
            };
        }

    }
}
