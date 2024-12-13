using Hexagon.Domain.Models.Contact_Us;
using Hexagon.Domain.ViewModels.Contact_Us;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Contact_Us
{
    public interface IContactUsRepository:IGenericRepository<ContactUs>
    {
        Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter);
    }
}
