using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.ViewModels.Contact_Us;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Contact_Us
{
    public interface IContactUsRepository:IGenericRepository<ContactUs>
    {
        Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
