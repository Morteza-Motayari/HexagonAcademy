using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Repositories.Users;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Infra.Data.Repositories.Gyms;

namespace Hexagon.Application.Services.Implementation.Gyms
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
                ModifiedDate = gym.LastModifiedDate
            };
            return Detail;
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

        public async Task<List<GymViewModel>?> ListGymsAsync()
        => await gymRepository.GetAllGymsAsync();

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
