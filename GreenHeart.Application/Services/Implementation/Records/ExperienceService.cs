using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Statics;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.Records;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using GreenHeart.Infra.Data.Repositories;
using GreenHeart.Infra.Data.Repositories.Gyms;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class ExperienceService(IExperienceRepository experienceRepository
        ,IUserRepository userRepository
        ,ICertificateRepository certificateRepository
        ,ICacheService cacheService,
        IRecordCategoryRepository recordCategoryRepository) : IExperienceService
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
                ExImage=experience.ExImage,
                UserId=experience.UserId,
                RecordCategoryId = experience.RecordCategoryId,
                RecordCategory = await recordCategoryRepository.GetCategoryNameAsync(experience.RecordCategoryId),
                UserName = await userRepository.GetJustUserName(experience.UserId),
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
                StaffId=model.StaffId,
                RecordCategoryId=model.RecordCategoryId
            };
            string slug = model.Title.GenerateSlug();
            if (await experienceRepository.ExistSpecificSlug(slug))
            {
                slug = await experienceRepository.PutSpecificSlug(slug);
            }
            experience.Slug = slug;
            #region Image
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.ExperiencePath);
                experience.ExImage = imageName;
            }
            #endregion

            await experienceRepository.InserAsync(experience);
            await experienceRepository.SaveChangeAsync();
            #region Deleting Cached Trainer
            string trainerSlug=await userRepository.GetStaffSlugByIdAsync(model.UserId);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.Trainer+trainerSlug);
            #endregion
            return CreateExperienceResult.Success;
        }

        public async Task<DeleteExperienceResult> DeleteExperienceAsync(int ExperienceId)
        {
            var experience = await experienceRepository.GetByIdAsync(ExperienceId);
            if (experience == null)
                return DeleteExperienceResult.ExperienceNotFound;
            if(experience.IsDeleted==true)
                return DeleteExperienceResult.ExperienceAlreadyDeleted;
            #region Deleting Image
            if (experience.ExImage != null)
            {
                experience.ExImage.DeleteImage(SavingPath.ExperiencePath);
                experience.ExImage = null;
            }
            #endregion
            experience.IsDeleted = true;
            experienceRepository.Update(experience);
            await experienceRepository.SaveChangeAsync();
            #region Deleting Cached Trainer
            string trainerSlug = await userRepository.GetStaffSlugByIdAsync(experience.UserId);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.Trainer + trainerSlug);
            #endregion
            return DeleteExperienceResult.Success;
        }

        public async Task<DeleteForeverExperienceResult> DeleteExperienceForever(int ExperienceId)
        {
            var experience = await experienceRepository.GetByIdAsync(ExperienceId);

            if (experience == null)
                return DeleteForeverExperienceResult.NotFound;

            if (experience.IsDeleted == false)
                return DeleteForeverExperienceResult.FirstDeleteSimple;
            DateTime lastDate = await experienceRepository.GetLastModifiedDate(ExperienceId);
            if (lastDate.SixMonthPassed())
            {
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
                IsDeleted=experience.IsDeleted,
                ExImage=experience.ExImage,
                RecordCategoryId=experience.RecordCategoryId
            };
        }

        public async Task<List<ExperienceViewModel>?> ListExperiencesForStaffCategoryAsync(int staffId,int categoryId)
        => await experienceRepository.GetAllExperiencesForStaffCategoryAsync(staffId,categoryId);

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
            #region Update Image
            if (model.NewImage != null)
            {
                if (experience.ExImage != null)
                {
                    experience.ExImage.DeleteImage(SavingPath.ExperiencePath);
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.ExperiencePath);
                experience.ExImage = imageName;
            }
            #endregion
            experienceRepository.Update(experience);
            await experienceRepository.SaveChangeAsync();
            #endregion

            #region Deleting Cached Trainer
            string trainerSlug = await userRepository.GetStaffSlugByIdAsync(experience.UserId);
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.Trainer + trainerSlug);
            #endregion

            return UpdateExperienceResult.Success;
        }

    }
}
