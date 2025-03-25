using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces
{
    public interface IExperienceRepository : IGenericRepository<Experience>
    {
        Task<bool> ExistSpecificSlug(string slug); 
        Task<string> PutSpecificSlug(string slug);
        Task<List<ExperienceViewModel>?> GetAllExperiencesAsync(int userId);
        Task<FilterExperienceViewModel> FilterExperienceAsync(FilterExperienceViewModel filter);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<List<ClientSideExperienceViewModel>> GetTrainerExperienceForClientSide(int staffId);
    }
}
