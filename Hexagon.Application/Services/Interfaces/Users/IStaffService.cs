using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;

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
    }
}
