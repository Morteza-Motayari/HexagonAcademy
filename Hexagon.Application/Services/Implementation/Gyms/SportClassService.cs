using Hexagon.Application.Convertors;
using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Statics;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Infra.Data.Repositories.Gyms;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class SportClassService(ISportClassRepository SportClassRepository
        ,IUserRepository userRepository,
        IGymRepository gymRepository,
        ISportRepository sportRepository,
        IStaffRepository staffRepository) : ISportClassService
    {
        public async Task<AdminSideDetailSportClassViewModel?> AdminSideDetailSportClassAsync(int SportClassId)
        {
            var sportClass = await SportClassRepository.GetSportClassWithDetails(SportClassId);
            if (sportClass == null)
                return null;
            AdminSideDetailSportClassViewModel? Detail = new()
            {
                Id = SportClassId,
                Title = sportClass.Title,
                GymId = sportClass.GymId,
                gym=sportClass.gym?.Name,
                TrainerId = sportClass.TrainerId,
                trainer=await staffRepository.GetStaffNameAsync(sportClass.TrainerId),
                SportId = sportClass.SportId,
                sport=sportClass.sport?.Title,
                StartDate = sportClass.StartDate,
                RegisteredAthletes=sportClass.ClassUsers.Count(),
                StartTime= sportClass.StartTime,
                EndTime = sportClass.EndTime,
                SubscriptionFee = sportClass.SubscriptionFee,
                MaxSubscription= sportClass.MaxSubscription,
                ImageUrl = sportClass.ImageUrl,
                Gender = sportClass.Gender,
                ClassStatus = sportClass.ClassStatus,
                CreatedDate = sportClass.CreatedDate,
                LastModifiedDate = sportClass.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(sportClass.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(sportClass.LastModifiedBy),
                CreatedById = sportClass.CreatedBy,
                LastModifiedById = sportClass.LastModifiedBy,
                IsDeleted = sportClass.IsDeleted
            };
            return Detail;
        }

        public async Task<CreateSportClassResult> CreateSportClassAsync(CreateSportClassViewModel model)
        {
            if(model.StartTime>=model.EndTime) 
                return CreateSportClassResult.InvalidEndTime;

            SportClass sportClass = new()
            {
                Title=model.Title,
                EndTime=model.EndTime,
                StartTime=model.StartTime,
                GymId=model.GymId,
                TrainerId=model.TrainerId,
                SportId=model.SportId,
                MaxSubscription=model.MaxSubscription,
                Gender=model.Gender,
                ClassStatus=model.ClassStatus,
                SubscriptionFee=model.SubscriptionFee
            };
            if (model.StartDate != null)
            {
                if (!model.StartDate.CheckPersianDate())
                {
                    return CreateSportClassResult.InvalidDateTime;
                }
                sportClass.StartDate = model.StartDate.ToMiladi();
            }
            string slug = model.Title.GenerateSlug();
            if (await SportClassRepository.ExistSpecificSlug(slug))
            {
                slug = await SportClassRepository.PutSpecificSlug(slug);
            }
            sportClass.Slug = slug;
            #region Image
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.SportClassPath);
                sportClass.ImageUrl = imageName;
            }
            #endregion

            await SportClassRepository.InserAsync(sportClass);
            await SportClassRepository.SaveChangeAsync();
            return CreateSportClassResult.Success;
        }

        public async Task<DeleteSportClassResult> DeleteSportClassAsync(int SportClassId)
        {
            var SportClass = await SportClassRepository.GetByIdAsync(SportClassId);
            if (SportClass == null)
                return DeleteSportClassResult.SportClassNotFound;
            if(SportClass.IsDeleted==true)
                return DeleteSportClassResult.SportClassAlreadyDeleted;

            #region Deleting Avatar
            if (SportClass.ImageUrl != null)
            {
                SportClass.ImageUrl.DeleteImage(SavingPath.SportClassPath);
                SportClass.ImageUrl=null;
            }
                
            #endregion

            SportClass.IsDeleted = true;
            SportClass.ClassStatus = SportClassStatus.Closed;
            SportClassRepository.Update(SportClass);
            await SportClassRepository.SaveChangeAsync();
            return DeleteSportClassResult.Success;
        }

        public async Task<FilterSportClassViewModel> FilterSportClassesAsync(FilterSportClassViewModel filter)
        => await SportClassRepository.FilterSportClassAsync(filter);

        public async Task<UpdateSportClassViewModel> GetSportClassForEdit(int SportClassId)
        {
            var SportClass = await SportClassRepository.GetByIdAsync(SportClassId);
            if (SportClass == null)
                return null;
            return new UpdateSportClassViewModel()
            {
                Id = SportClass.Id,
                StartDate= SportClass.StartDate.ToShamsi(),
                StartTime= SportClass.StartTime,
                EndTime= SportClass.EndTime,
                Title = SportClass.Title,
                GymId = SportClass.GymId,
                TrainerId = SportClass.TrainerId,
                SportId = SportClass.SportId,
                SubscriptionFee = SportClass.SubscriptionFee,
                ImageUrl = SportClass.ImageUrl,
                IsDeleted = SportClass.IsDeleted,
                MaxSubscription= SportClass.MaxSubscription,
                Gender= SportClass.Gender,
                ClassStatus = SportClass.ClassStatus
            };
        }

        public async Task<List<SportClassViewModel>?> ListSportClassesAsync()
        => await SportClassRepository.GetAllSportClasssAsync();

        public async Task<UpdateSportClassResult> UpdateSportClassAsync(UpdateSportClassViewModel model)
        {
            if (model.StartTime >= model.EndTime)
                return UpdateSportClassResult.InvalidEndTime;

            var SportClass = await SportClassRepository.GetByIdAsync(model.Id);
            if (SportClass == null)
                return UpdateSportClassResult.SportClassNotFound;

            #region Update SportClass
            if (model.StartDate != null)
            {
                if (!model.StartDate.CheckPersianDate())
                {
                    return UpdateSportClassResult.InvalidDateTime;
                }
                SportClass.StartDate = model.StartDate.ToMiladi();
            }
            SportClass.StartTime = model.StartTime;
            SportClass.EndTime = model.EndTime;
            SportClass.GymId = model.GymId;
            SportClass.TrainerId = model.TrainerId;
            SportClass.SportId = model.SportId;
            SportClass.SubscriptionFee = model.SubscriptionFee;
            if (SportClass.Title != model.Title)
            {
                SportClass.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                if (await SportClassRepository.ExistSpecificSlug(slug))
                {
                    slug = await SportClassRepository.PutSpecificSlug(slug);
                }
                SportClass.Slug = slug;
            }            
            #region Update Image
            if (model.NewImage != null)
            {
                if (SportClass.ImageUrl != null)
                {
                    SportClass.ImageUrl.DeleteImage(SavingPath.AvatarPath);
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.SportClassPath);
                SportClass.ImageUrl = imageName;
            }
            #endregion
            SportClassRepository.Update(SportClass);
            await SportClassRepository.SaveChangeAsync();
            #endregion

            return UpdateSportClassResult.Success;
        }

    }
}
