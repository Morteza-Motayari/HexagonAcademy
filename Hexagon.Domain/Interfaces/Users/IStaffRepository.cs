using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Hexagon.Domain.ViewModels.Users.Users;
using System.Collections.ObjectModel;

namespace Hexagon.Domain.Interfaces
{
    public interface IStaffRepository:IGenericRepository<Staff>
    {
        Task<bool> DuplicatedStaffPositionAsync(string position, int userId);
        Task<bool> DuplicatedStaffPositionAsync(string position, int userId,int staffId);
        Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter);
        Task<Staff?> GetStaffWithUser(int staffId);
        Task<bool> ExistStaffForUser(int userId);
        Task<List<int>> GetUserStaffIds(int userId);
        ReadOnlyCollection<TrainerViewModel>? ListSuitableTrainersForClassAsync(UserGender gender,int sportCertificateId);
        Task<List<TrainerViewModel>?> ListSuitableTrainersForEditClassAsync(UserGender gender,int sportCertificateId);
        Task<string> GetStaffNameAsync(int staffId);
        Task<FilterCaderViewModel>FilterCadersAsync(FilterCaderViewModel filter);
        Task<List<CaderViewModel>> GetCadersWithRoleAsync(int roleId);
        Task<bool> ExistActiveCaderForUser(int userId);
        Task<ClientSideTrainerForClass?> GetTrainerNameAndImageAsync(int staffId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
