using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Gyms
{
    public interface ISportRepository : IGenericRepository<Sport>
    {
        Task<bool> ExistSportTitle(string title);
        Task<bool> ExistSportTitle(string title,int id);
        Task<FilterSportViewModel> FilterSportAsync(FilterSportViewModel filter);
        Task<List<SportViewModel>?> GetAllSportsAsync();
        Task<Sport?> GetSportWithCertificate(int id);
    }
}
