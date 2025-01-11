using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Records.Experiences;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Infra.Data.Repositories.Gyms;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class ExperienceService(IExperienceRepository experienceRepository
        ,IUserRepository userRepository
        ,ICertificateRepository certificateRepository) : IExperienceService
    {
        public async Task<AdminSideDetailExperienceViewModel?> AdminSideDetailExperienceAsync(int ExperienceId)
        {
            var experience = await experienceRepository.GetByIdAsync(ExperienceId);
            if (experience == null)
                return null;
            AdminSideDetailExperienceViewModel? Detail = new()
            {
                Id = ExperienceId,
                Certificate=experience.CertificateId!=null?await certificateRepository.GetCertificateTitle((int)experience.CertificateId):string.Empty,
                Detail=experience.Detail,
                HowLong=experience.HowLong,
                CertificateId=experience.CertificateId,
                Company=experience.Company,
                StaffId=experience.StaffId,
                Title=experience.Title,
                UserId=experience.UserId,
                UserName= await userRepository.GetJustUserName(experience.UserId),
                CreatedDate = experience.CreatedDate,
                LastModifiedDate = experience.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(experience.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(experience.LastModifiedBy),
                CreatedById = experience.CreatedBy,
                LastModifiedById = experience.LastModifiedBy,
                IsDeleted = experience.IsDeleted
            };
            return Detail;
        }

        public async Task<string> CantDeleteExperienceForeverNowMessage(int ExperienceId)
        {
            DateTime lastEdit = await experienceRepository.GetLastModifiedDate(ExperienceId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این سابقه تا {leftdays} روز آینده را ندارید.";
        }

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
                UserId=model.UserId,
                StaffId=model.StaffId
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
            if(experience.IsDeleted==true)
                return DeleteExperienceResult.ExperienceAlreadyDeleted;

            experience.IsDeleted = true;
            experienceRepository.Update(experience);
            await experienceRepository.SaveChangeAsync();
            return DeleteExperienceResult.Success;
        }

        public async Task<DeleteForeverExperienceResult> DeleteExperienceForever(int ExperienceId)
        {
            DateTime lastDate = await experienceRepository.GetLastModifiedDate(ExperienceId);
            if (lastDate.SixMonthPassed())
            {
                var experience = await experienceRepository.GetByIdAsync(ExperienceId);

                if (experience == null)
                    return DeleteForeverExperienceResult.NotFound;

                if (experience.IsDeleted == false)
                    return DeleteForeverExperienceResult.FirstDeleteSimple;

                experienceRepository.Delete(experience);
                await experienceRepository.SaveChangeAsync();
                return DeleteForeverExperienceResult.Success;
            }
            else
            {
                return DeleteForeverExperienceResult.CantDeletedNow;
            }
        }

        public async Task<FilterExperienceViewModel> FilterExperienceesAsync(FilterExperienceViewModel filter)
        => await experienceRepository.FilterExperienceAsync(filter);

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
                UserId=experience.UserId,
                StaffId=experience.StaffId,
                IsDeleted=experience.IsDeleted
            };
        }

        public async Task<List<ExperienceViewModel>?> ListExperienceesAsync(int staffId)
        => await experienceRepository.GetAllExperiencesAsync(staffId);

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
