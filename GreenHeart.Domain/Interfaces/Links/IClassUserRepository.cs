using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Links
{
    public interface IClassUserRepository : IGenericRepository<ClassUser>
    {
        Task<List<ClassUser>?> GetClassUsersAsync(int classId);
        Task<List<int>?> GetClassUserIdsAsync(int classId);
        Task<List<int>?> GetClassUsersIdentityKeyAsync(int classId);
        Task<ClassUser?> GetClassUserAsync(int id);
        Task DeleteClassUser(int id);
        Task DeleteClassUsers(int classId);
        void Remove(ClassUser classUser);
        Task<int> RegisteredUserLastMonth(int classId);
        Task<bool> IsUserRegisteredInClass(int userId,int classId);
        Task<DateTime> LastUserRegistrationDateInClass(int userId,int classId);
        Task<UserSideFilterSportClassViewModel> GetUserClassesAsync(int userId, UserSideFilterSportClassViewModel filter);
        DateTime RegistraionDate(int userId, int classId);
        Task<FilterSportClassAthleteViewModel> FilterSportClassAthlete(FilterSportClassAthleteViewModel filter);

    }
}
