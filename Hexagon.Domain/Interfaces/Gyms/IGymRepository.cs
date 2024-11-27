using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Gyms
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
    }
}
