using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using GreenHeart.Domain.ViewModels.Users.Roles;

namespace GreenHeart.Application.Services.Interfaces.Gyms
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
        Task<FilterSportClassAthleteViewModel> FilterSportClassAthleteAsync(FilterSportClassAthleteViewModel filter);
        #endregion

        #region Client
        Task<ClientSideFilterSportClassViewModel> ClientSideFilterClasses(ClientSideFilterSportClassViewModel filter);
        Task<ClientSideSportClassDeatilViewModel> ClientSideSportClassViewModel(string slug);
        Task<UserSideFilterSportClassViewModel> GetUserSportClassesAsync(int userId, UserSideFilterSportClassViewModel filter);
        Task<List<ClientSideSportClassViewModel>?> GetClassesForIndexPage(FilterUserGender gender);
        #endregion
    }
}
