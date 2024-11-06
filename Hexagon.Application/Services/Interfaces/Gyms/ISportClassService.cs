using Hexagon.Domain.ViewModels.Gyms.SportClasses;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface ISportClassService
    {
        Task<CreateSportClassResult> CreateSportClassAsync(CreateSportClassViewModel model);
        Task<UpdateSportClassViewModel> GetSportClassForEdit(int sportClassId);
        Task<UpdateSportClassResult> UpdateSportClassAsync(UpdateSportClassViewModel model);
        Task<DeleteSportClassResult> DeleteSportClassAsync(int sportClassId);
        Task<List<SportClassViewModel>?> ListSportClassesAsync();
        Task<FilterSportClassViewModel> FilterSportClassesAsync(FilterSportClassViewModel filter);
    }
}
