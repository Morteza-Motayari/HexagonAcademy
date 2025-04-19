using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Staffs.Caders;
using GreenHeart.Domain.ViewModels.Users.Staffs.Trainers;
using GreenHeart.Domain.ViewModels.Users.Users;
using System.Collections.ObjectModel;

namespace GreenHeart.Domain.Interfaces
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
        Task<Staff?> GetStaffByUserSlug(string slug);
        Task<int> GetUserIdByStaffId(int staffId);
    }
}
