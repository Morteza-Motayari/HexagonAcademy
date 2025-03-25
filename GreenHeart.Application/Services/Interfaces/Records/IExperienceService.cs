using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Records.Experiences;

namespace GreenHeart.Application.Services.Interfaces.Records
{
    public interface IExperienceService
    {
        Task<CreateExperienceResult> CreateExperienceAsync(CreateExperienceViewModel model);
        Task<UpdateExperienceViewModel> GetExperienceForEdit(int experienceId);
        Task<UpdateExperienceResult> UpdateExperienceAsync(UpdateExperienceViewModel model);
        Task<DeleteExperienceResult> DeleteExperienceAsync(int experienceId);
        Task<List<ExperienceViewModel>?> ListExperienceesAsync(int staffId);
        Task<FilterExperienceViewModel> FilterExperienceesAsync(FilterExperienceViewModel filter);
        Task<AdminSideDetailExperienceViewModel?> AdminSideDetailExperienceAsync(int ExperienceId);
        Task<DeleteForeverExperienceResult> DeleteExperienceForever(int ExperienceId);
        Task<string> CantDeleteExperienceForeverNowMessage(int ExperienceId);

    }
}
