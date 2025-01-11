using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Hexagon.Domain.ViewModels.Users.Users;
using System.Collections.ObjectModel;

namespace Hexagon.Application.Services.Interfaces.Users
{
    public interface IStaffService
    {
        #region Trainer
        Task<CreateTrainerResult> CreateTrainerAsync(CreateTrainerViewModel model);
        Task<UpdateTrainerViewModel?> GetTrainerForEdit(int TrainerId);
        Task<UpdateTrainerResult> UpdateTrainerAsync(UpdateTrainerViewModel model);
        Task<DeleteTrainerResult> DeleteTrainerAsync(int TrainerId);
        Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter);
        Task<AdminSideDetailTrainerViewModel?> AdminSideDetailTrainerAsync(int TrainerId);
        Task<ReadOnlyCollection<TrainerViewModel>?> ListTrainerForItemsAsync(UserGender gender, int sportId);
        Task<List<TrainerViewModel>?> ListTrainerForEditItemsAsync(UserGender gender, int sportId);
        Task<TrainerViewModel> GetTrainerWithName(int trainerId);
        Task<DeleteForeverTrainerResult> DeleteTrainerForever(int TrainerId);
        Task<string> CantDeleteTrainerForeverNowMessage(int TrainerId);
        Task<ClientSideFilterTrainerViewModel> ClientSideFilterTrainerAsync(ClientSideFilterTrainerViewModel filter);
        Task<List<ClientSideTrainerViewModel>> GetTrainersForHomePageAsync();
        Task<ClientSideTrainerDetailViewModel?> GetTrainerDetailAsync(string slug);
        #endregion

        #region Cader
        Task<CreateCaderResult> CreateCaderAsync(CreateCaderViewModel model);
        Task<UpdateCaderViewModel?> GetCaderForEdit(int CaderId);
        Task<UpdateCaderResult> UpdateCaderAsync(UpdateCaderViewModel model);
        Task<DeleteCaderResult> DeleteCaderAsync(int CaderId);
        Task<FilterCaderViewModel> FilterCadersAsync(FilterCaderViewModel filter);
        Task<AdminSideDetailCaderViewModel?> AdminSideDetailCaderAsync(int CaderId);
        Task<bool> UserHasPermission(int userId);
        Task<DeleteForeverCaderResult> DeleteCaderForever(int CaderId);
        Task<string> CantDeleteCaderForeverNowMessage(int CaderId);
        Task<List<ClientSideCaderViewModel>?> GetCadersForAbouUsPageAsync();
        #endregion
    }
}
