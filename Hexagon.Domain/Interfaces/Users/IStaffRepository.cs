using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;

namespace Hexagon.Domain.Interfaces
{
    public interface IStaffRepository:IGenericRepository<Staff>
    {
        Task<bool> DuplicatedStaffPositionAsync(string position, int userId);
        Task<bool> DuplicatedStaffPositionAsync(string position, int userId,int staffId);
        Task<FilterTrainerViewModel> FilteTrainersAsync(FilterTrainerViewModel filter);
        Task<Staff?> GetStaffWithUser(int staffId);
        Task<bool> ExistStaffForUser(int userId);
        Task<List<int>> GetUserStaffIds(int userId);
    }
}
