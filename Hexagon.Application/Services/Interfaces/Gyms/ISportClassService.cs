using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.Users.Roles;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface ISportClassService
    {
        #region Admin
        Task<CreateSportClassResult> CreateSportClassAsync(CreateSportClassViewModel model);
        Task<UpdateSportClassViewModel> GetSportClassForEdit(int sportClassId);
        Task<UpdateSportClassResult> UpdateSportClassAsync(UpdateSportClassViewModel model);
        Task<DeleteSportClassResult> DeleteSportClassAsync(int sportClassId);
        Task<List<SportClassViewModel>?> ListSportClassesAsync();
        Task<FilterSportClassViewModel> FilterSportClassesAsync(FilterSportClassViewModel filter);
        Task<AdminSideDetailSportClassViewModel?> AdminSideDetailSportClassAsync(int SportClassId);
        Task<DeleteForeverSportClassResult> DeleteSportClassForever(int SportClassId);
        Task<string> CantDeleteSportClassForeverNowMessage(int SportClassId);
        #endregion

        #region Client
        Task<ClientSideFilterSportClassViewModel> ClientSideFilterClasses(ClientSideFilterSportClassViewModel filter);
        Task<ClientSideSportClassDeatilViewModel> ClientSideSportClassViewModel(string slug);
        #endregion
    }
}
