using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.Records.Certificates;

namespace Hexagon.Application.Services.Interfaces.Gyms
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

    }
}
