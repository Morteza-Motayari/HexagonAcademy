using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Gyms
{
    public interface IGymRepository : IGenericRepository<Gym>
    {
        Task<bool> ExistSpecificSlug(string slug);
        Task<string> PutSpecificSlug(string slug);
        Task<bool> ExistConstantPhoneNumberAsync(string constantphone);
        Task<bool> ExistConstantPhoneNumberAsync(string constantphone,int gymId);
        Task<FilterGymViewModel> FilterGymAsync(FilterGymViewModel filter);
        Task<List<GymViewModel>?> GetAllGymsAsync();
        Task<bool> ExistGymAsync(int gymId);
        Task<List<GymViewModel>?> GetAllGymItemsAsync();
        string GetGymName(int gymId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
