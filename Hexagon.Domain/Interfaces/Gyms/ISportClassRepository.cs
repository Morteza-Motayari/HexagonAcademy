using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Gyms
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

    }
}
