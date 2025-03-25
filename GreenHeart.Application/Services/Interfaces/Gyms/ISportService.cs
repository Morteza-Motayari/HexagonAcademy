using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Users.Roles;

namespace GreenHeart.Application.Services.Interfaces.Gyms
{
    public interface ISportService
    {
        Task<CreateSportResult> CreateSportAsync(CreateSportViewModel model);
        Task<UpdateSportViewModel> GetSportForEdit(int SportId);
        Task<UpdateSportResult> UpdateSportAsync(UpdateSportViewModel model);
        Task<DeleteSportResult> DeleteSportAsync(int SportId);
        Task<List<SportViewModel>?> ListSportsForOptionsAsync();
        Task<FilterSportViewModel> FilterSportsAsync(FilterSportViewModel filter);
        Task<AdminSideDetailSportViewModel?> AdminSideDetailSportAsync(int SportId);
        Task<List<ClientSideSportExisted>?> GetSportExistedAsync();
        Task<DeleteForeverSportResult> DeleteSportForever(int SportId);
        Task<string> CantDeleteSportForeverNowMessage(int SportId);
        Task<List<ClientSideSportNameViewModel>?> GetActiveSportNameAsync();

    }
}
