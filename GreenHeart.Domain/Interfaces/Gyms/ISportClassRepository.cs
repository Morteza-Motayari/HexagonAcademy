using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Gyms
{
    public interface ISportClassRepository : IGenericRepository<SportClass>
    {
        Task<bool> ExistSpecificSlug(string slug);
        Task<string> PutSpecificSlug(string slug);
        Task<FilterSportClassViewModel> FilterSportClassAsync(FilterSportClassViewModel filter);
        Task<List<SportClassViewModel>?> GetAllSportClasssAsync();
        Task<SportClass?> GetSportClassWithDetails(int sportClassId);
        Task<ClientSideFilterSportClassViewModel> ClientSideFilterClasses(ClientSideFilterSportClassViewModel filter);
        Task<SportClass?> GetClassBySlugAsync(string slug);
        Task<string?> getSportClassName(int sportClassId);
        Task<int> GetMaxClassAthleteSpace(int sportClassId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<UserSideFilterSportClassViewModel> GetUserClassesAsync(int userId, UserSideFilterSportClassViewModel filter);
        Task<List<ClientSideSportClassViewModel>?> GetClassesForIndexPage(FilterUserGender filter);
        // Task<FilterSportClassAthleteViewModel> FilterSportClassAthlete(FilterSportClassAthleteViewModel filter)
        Task<string?> GetClassSlugByIdAsync(int sportClassId);
    }
}
