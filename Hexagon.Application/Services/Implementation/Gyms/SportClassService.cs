using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class SportClassService(ISportClassRepository SportClassRepository) : ISportClassService
    {
        public async Task<CreateSportClassResult> CreateSportClassAsync(CreateSportClassViewModel model)
        {

            SportClass sportClass = new()
            {
                CreatedDate = DateTime.Now,
                Title=model.Title,
                StartDate = model.StartDate,
                EndTime=model.EndTime,
                StartTime=model.StartTime,
                GymId=model.GymId,
                TrainerId=model.TrainerId,
                SportId=model.SportId
            };
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

            #region Deleting Avatar
            if (SportClass.ImageUrl != null)
                SportClass.ImageUrl.DeleteImage(SavingPath.SportClassPath);
            #endregion

            SportClass.IsDeleted = true;
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
                StartDate= SportClass.StartDate,
                StartTime= SportClass.StartTime,
                EndTime= SportClass.EndTime,
                Title = SportClass.Title,
                GymId = SportClass.GymId,
                TrainerId = SportClass.TrainerId,
                SportId = SportClassId,
                SubscriptionFee = SportClass.SubscriptionFee,
                ImageUrl = SportClass.ImageUrl
            };
        }

        public async Task<List<SportClassViewModel>?> ListSportClassesAsync()
        => await SportClassRepository.GetAllSportClasssAsync();

        public async Task<UpdateSportClassResult> UpdateSportClassAsync(UpdateSportClassViewModel model)
        {
            var SportClass = await SportClassRepository.GetByIdAsync(model.Id);
            if (SportClass == null)
                return UpdateSportClassResult.SportClassNotFound;

            #region Update SportClass
            SportClass.StartDate = model.StartDate;
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
            SportClassRepository.Update(SportClass);
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
            await SportClassRepository.SaveChangeAsync();
            #endregion

            return UpdateSportClassResult.Success;
        }

    }
}
