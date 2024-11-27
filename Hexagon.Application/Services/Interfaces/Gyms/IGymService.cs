using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Users.Users;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface IGymService
    {
        Task<CreateGymResult> CreateGymAsync(CreateGymViewModel model);
        Task<UpdateGymViewModel> GetGymForEdit(int GymId);
        Task<UpdateGymResult> UpdateGymAsync(UpdateGymViewModel model);
        Task<DeleteGymResult> DeleteGymAsync(int GymId);
        Task<List<GymViewModel>?> ListGymsAsync();
        Task<FilterGymViewModel> FilterGymsAsync(FilterGymViewModel filter);
        Task<AdminSideDetailGymViewModel?> AdminSideDetailGymAsync(int gymId);
    }
}
