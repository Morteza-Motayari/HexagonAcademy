using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.ViewModels.Records.Experiences;
using Hexagon.Infra.Data.Repositories.Gyms;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class ExperienceService(IExperienceRepository experienceRepository) : IExperienceService
    {
        public async Task<CreateExperienceResult> CreateExperienceAsync(CreateExperienceViewModel model)
        {

            Experience experience = new()
            {
                CreatedDate = DateTime.Now,
                CertificateId = model.CertificateId,
                Detail=model.Detail,
                Company=model.Company,
                Title=model.Title,
                HowLong=model.HowLong,
                UserId=model.UserId
            };
            string slug = model.Title.GenerateSlug();
            if (await experienceRepository.ExistSpecificSlug(slug))
            {
                slug = await experienceRepository.PutSpecificSlug(slug);
            }
            experience.Slug = slug;
            await experienceRepository.InserAsync(experience);
            await experienceRepository.SaveChangeAsync();
            return CreateExperienceResult.Success;
        }

        public async Task<DeleteExperienceResult> DeleteExperienceAsync(int ExperienceId)
        {
            var experience = await experienceRepository.GetByIdAsync(ExperienceId);
            if (experience == null)
                return DeleteExperienceResult.ExperienceNotFound;

            experience.IsDeleted = true;
            experienceRepository.Update(experience);
            await experienceRepository.SaveChangeAsync();
            return DeleteExperienceResult.Success;
        }

        public async Task<FilterExperienceViewModel> FilterExperienceesAsync(FilterExperienceViewModel filter,int userId)
        => await experienceRepository.FilterExperienceAsync(filter,userId);

        public async Task<UpdateExperienceViewModel> GetExperienceForEdit(int ExperienceId)
        {
            var experience = await experienceRepository.GetByIdAsync(ExperienceId);
            if (experience == null)
                return null;
            return new UpdateExperienceViewModel()
            {
                Id = experience.Id,
                Title = experience.Title,
                CertificateId = experience.CertificateId,
                Company=experience.Company,
                Detail=experience.Detail,
                HowLong=experience.HowLong,
                UserId=experience.UserId
            };
        }

        public async Task<List<ExperienceViewModel>?> ListExperienceesAsync(int userId)
        => await experienceRepository.GetAllExperiencesAsync(userId);

        public async Task<UpdateExperienceResult> UpdateExperienceAsync(UpdateExperienceViewModel model)
        {
            var experience = await experienceRepository.GetByIdAsync(model.Id);
            if (experience == null)
                return UpdateExperienceResult.ExperienceNotFound;

            #region Update Experience
            experience.Detail = model.Detail;
            experience.Company = model.Company;
            experience.HowLong = model.HowLong;
            experience.CertificateId = model.CertificateId;
            if (experience.Title != model.Title)
            {
                experience.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                if (await experienceRepository.ExistSpecificSlug(slug))
                {
                    slug = await experienceRepository.PutSpecificSlug(slug);
                }
                experience.Slug = slug;
            }
            experienceRepository.Update(experience);
            await experienceRepository.SaveChangeAsync();
            #endregion

            return UpdateExperienceResult.Success;
        }

    }
}
