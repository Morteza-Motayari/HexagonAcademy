using GreenHeart.Domain.ViewModels.Contact_Us;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Contact_Us
{
    public interface IContactUsService
    {
        Task<CreateContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model);
        Task<AnswerContactUsViewModel> GetContactUsForAnswer(int ContactUsId);
        Task<AnswerContactUsResult> AnswerContactUs(AnswerContactUsViewModel ContactUs);
        Task<DeleteContactUsResult> DeleteContactUsAsync(int ContactUsId);
        Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter);
        Task<AdminSideDetailContactUsViewModel?> AdminSideDetailContactUsAsync(int ContactUsId);
        Task<DeleteForeverContactUsResult> DeleteContactUsForever(int ContactUsId);
        Task<string> CantDeleteContactUsForeverNowMessage(int ContactUsId);
    }
}
