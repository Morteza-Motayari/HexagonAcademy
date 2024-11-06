using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Records.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces
{
    public interface IExperienceRepository : IGenericRepository<Experience>
    {
        Task<bool> ExistSpecificSlug(string slug); 
        Task<string> PutSpecificSlug(string slug);
        Task<List<ExperienceViewModel>?> GetAllExperiencesAsync(int userId);
        Task<FilterExperienceViewModel> FilterExperienceAsync(FilterExperienceViewModel filter, int userId);
    }
}
