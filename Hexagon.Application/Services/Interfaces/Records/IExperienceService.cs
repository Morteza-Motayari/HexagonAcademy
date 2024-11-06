using Hexagon.Domain.ViewModels.Records.Experiences;

namespace Hexagon.Application.Services.Interfaces.Records
{
    public interface IExperienceService
    {
        Task<CreateExperienceResult> CreateExperienceAsync(CreateExperienceViewModel model);
        Task<UpdateExperienceViewModel> GetExperienceForEdit(int experienceId);
        Task<UpdateExperienceResult> UpdateExperienceAsync(UpdateExperienceViewModel model);
        Task<DeleteExperienceResult> DeleteExperienceAsync(int experienceId);
        Task<List<ExperienceViewModel>?> ListExperienceesAsync(int userId);
        Task<FilterExperienceViewModel> FilterExperienceesAsync(FilterExperienceViewModel filter,int userId);
    }
}
