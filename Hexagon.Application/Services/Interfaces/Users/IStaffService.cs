using Hexagon.Domain.Enums.Users;
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
        #endregion

        #region Cader
        Task<CreateCaderResult> CreateCaderAsync(CreateCaderViewModel model);
        Task<UpdateCaderViewModel?> GetCaderForEdit(int CaderId);
        Task<UpdateCaderResult> UpdateCaderAsync(UpdateCaderViewModel model);
        Task<DeleteCaderResult> DeleteCaderAsync(int CaderId);
        Task<FilterCaderViewModel> FilterCadersAsync(FilterCaderViewModel filter);
        Task<AdminSideDetailCaderViewModel?> AdminSideDetailCaderAsync(int CaderId);
        Task<bool> UserHasPermission(int userId);
        #endregion
    }
}
