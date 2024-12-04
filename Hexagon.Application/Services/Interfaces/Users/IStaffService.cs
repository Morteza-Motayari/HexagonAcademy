using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Hexagon.Domain.ViewModels.Users.Users;
using System.Collections.ObjectModel;

namespace Hexagon.Application.Services.Interfaces.Users
{
    public interface IStaffService
    {
        Task<CreateTrainerResult> CreateTrainerAsync(CreateTrainerViewModel model);
        Task<UpdateTrainerViewModel?> GetTrainerForEdit(int TrainerId);
        Task<UpdateTrainerResult> UpdateTrainerAsync(UpdateTrainerViewModel model);
        Task<DeleteTrainerResult> DeleteTrainerAsync(int TrainerId);
        Task<List<TrainerViewModel>?> ListTrainersAsync();
        Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter);
        Task<AdminSideDetailTrainerViewModel?> AdminSideDetailTrainerAsync(int TrainerId);
        Task<ReadOnlyCollection<TrainerViewModel>?> ListTrainerForItemsAsync(UserGender gender,int sportId);
        Task<List<TrainerViewModel>?> ListTrainerForEditItemsAsync(UserGender gender,int sportId);
        Task<TrainerViewModel> GetTrainerWithName(int trainerId);
    }
}
