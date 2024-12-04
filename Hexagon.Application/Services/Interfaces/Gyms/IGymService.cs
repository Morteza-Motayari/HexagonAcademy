using Hexagon.Domain.ViewModels.Gyms.Gyms;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface IGymService
    {
        Task<CreateGymResult> CreateGymAsync(CreateGymViewModel model);
        Task<UpdateGymViewModel> GetGymForEdit(int GymId);
        Task<UpdateGymResult> UpdateGymAsync(UpdateGymViewModel model);
        Task<DeleteGymResult> DeleteGymAsync(int GymId);
        Task<List<GymViewModel>?> ListGymsForOptionsAsync();
        Task<FilterGymViewModel> FilterGymsAsync(FilterGymViewModel filter);
        Task<AdminSideDetailGymViewModel?> AdminSideDetailGymAsync(int gymId);
    }
}
