using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Application.Statics;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using GreenHeart.Domain.ViewModels.Users.Users;
using GreenHeart.Infra.Data.Repositories.Users;
using GreenHeart.Infra.Data.Repositories;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Domain.Interfaces.Users;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class GymService(IGymRepository gymRepository,IUserRepository userRepository) : IGymService
    {
        public async Task<AdminSideDetailGymViewModel?> AdminSideDetailGymAsync(int gymId)
        {
            var gym = await gymRepository.GetByIdAsync(gymId);
            if (gym == null)
                return null;

            AdminSideDetailGymViewModel? Detail = new()
            {
                Id = gymId,
                Name = gym.Name,
                Address = gym.Address,
                Area = gym.Area,
                ImageUrl = gym.ImageUrl,
                ConstantPhone = gym.ConstantPhone,
                IsDeleted = gym.IsDeleted,
                CreatedById = gym.CreatedBy,
                LastModifiedById = gym.LastModifiedBy,
                CreatedDate = gym.CreatedDate,
                CreatedBy = await userRepository.GetJustUserName(gym.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(gym.LastModifiedBy),
                LastModifiedDate = gym.LastModifiedDate
            };
            return Detail;
        }

        public async Task<string> CantDeleteGymForeverNowMessage(int GymId)
        {
            DateTime lastEdit = await gymRepository.GetLastModifiedDate(GymId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این باشگاه تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateGymResult> CreateGymAsync(CreateGymViewModel model)
        {
            if (await gymRepository.ExistConstantPhoneNumberAsync(model.ConstantPhone))
                return CreateGymResult.DuplicatedConstantPhone;

            Gym gym = new()
            {
                CreatedDate = DateTime.Now,
                Address = model.Address,
                Name = model.Name,
                ConstantPhone = model.ConstantPhone,
                Area = model.Area
            };
            string slug = model.Name.GenerateSlug();
            if (await gymRepository.ExistSpecificSlug(slug))
            {
                slug = await gymRepository.PutSpecificSlug(slug);
            }
            gym.Slug = slug;
            #region Image
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.GymPath);
                gym.ImageUrl = imageName;
            }
            #endregion

            await gymRepository.InserAsync(gym);
            await gymRepository.SaveChangeAsync();
            return CreateGymResult.Success;
        }

        public async Task<DeleteGymResult> DeleteGymAsync(int GymId)
        {
            var Gym = await gymRepository.GetByIdAsync(GymId);
            if (Gym == null)
                return DeleteGymResult.GymNotFound;
            if (Gym.IsDeleted == true)
                return DeleteGymResult.UserAlreadyDeleted;

            #region Deleting Image
            if (Gym.ImageUrl != null)
            {
                Gym.ImageUrl.DeleteImage(SavingPath.GymPath);
                Gym.ImageUrl = null;
            }               
            #endregion

            Gym.IsDeleted = true;
            gymRepository.Update(Gym);
            await gymRepository.SaveChangeAsync();
            return DeleteGymResult.Success;
        }

        public async Task<DeleteForeverGymResult> DeleteGymForever(int GymId)
        {
            DateTime lastDate = await gymRepository.GetLastModifiedDate(GymId);
            if (lastDate.SixMonthPassed())
            {
                var gym = await gymRepository.GetByIdAsync(GymId);

                if (gym == null)
                    return DeleteForeverGymResult.NotFound;

                if (gym.IsDeleted == false)
                    return DeleteForeverGymResult.FirstDeleteSimple;

                gymRepository.Delete(gym);
                await gymRepository.SaveChangeAsync();
                return DeleteForeverGymResult.Success;
            }
            else
            {
                return DeleteForeverGymResult.CantDeletedNow;
            }
        }

        public async Task<FilterGymViewModel> FilterGymsAsync(FilterGymViewModel filter)
        => await gymRepository.FilterGymAsync(filter);

        public async Task<UpdateGymViewModel> GetGymForEdit(int GymId)
        {
            var Gym = await gymRepository.GetByIdAsync(GymId);
            if (Gym == null)
                return null;
            return new UpdateGymViewModel()
            {
                Id = Gym.Id,
                Address = Gym.Address,
                ConstantPhone = Gym.ConstantPhone,
                ImageUrl = Gym.ImageUrl,
                Name = Gym.Name,
                Area = Gym.Area,
                IsDeleted= Gym.IsDeleted
            };
        }


        public async Task<List<GymViewModel>?> ListGymsForOptionsAsync()
        => await gymRepository.GetAllGymItemsAsync();

        public async Task<UpdateGymResult> UpdateGymAsync(UpdateGymViewModel model)
        {
            var gym = await gymRepository.GetByIdAsync(model.Id);
            if (gym == null)
                return UpdateGymResult.GymNotFound;
            if (await gymRepository.ExistConstantPhoneNumberAsync(model.ConstantPhone,model.Id))
                return UpdateGymResult.DuplicatedConstantPhone;


            #region Update Gym
            gym.Address = model.Address;
            gym.Area = model.Area;
            gym.ConstantPhone = model.ConstantPhone;
            if (gym.Name != model.Name)
            {
                gym.Name = model.Name;
                string slug = model.Name.GenerateSlug();
                if (await gymRepository.ExistSpecificSlug(slug))
                {
                    slug = await gymRepository.PutSpecificSlug(slug);
                }
                gym.Slug = slug;
            }           
            #region Update Image
            if (model.NewImage != null)
            {
                if (gym.ImageUrl != null)
                {
                    gym.ImageUrl.DeleteImage(SavingPath.GymPath);
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.GymPath);
                gym.ImageUrl = imageName;
            }
            #endregion
            gymRepository.Update(gym);
            await gymRepository.SaveChangeAsync();
            #endregion

            return UpdateGymResult.Success;
        }

    }
}
