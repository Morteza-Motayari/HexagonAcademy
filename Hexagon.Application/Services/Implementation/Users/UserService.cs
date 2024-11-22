using Hexagon.Application.Extensions;
using Hexagon.Application.Security;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Statics;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class UserService(IUserRepository UserRepository,
        IUserRoleRepository userRoleRepository) : IUserService
    {
        public async Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model)
        {
            if (await UserRepository.ExistMobileAsync(model.PhoneNumber))
                return CreateUserResult.MobileDuplicated;
            if (await UserRepository.ExistNationalCodeAsync(model.NationalCode))
                return CreateUserResult.NationalCodeDuplicated;

            User User = new()
            {
                CreatedDate = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                city = model.city,
                NationalCode = model.NationalCode,
                Password = model.Password.EncodePasswordMd5(),
                PhoneNumber = model.PhoneNumber,
                email = model.email,
                BirthDay = model.BirthDay,
                Gender = model.Gender,
                Situation = model.Situation,
                Status = UserStatus.Active,
                
            };

            #region Avatar
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.AvatarPath);
                User.Avatar = imageName;
            }
            #endregion

            await UserRepository.InserAsync(User);
            await UserRepository.SaveChangeAsync();

            #region Add User Roles
            foreach(var role in model.RolesId)
            {
                await userRoleRepository.InserAsync(new UserRole { RoleId = role,UserId=User.Id });
            }
            await userRoleRepository.SaveChangeAsync();
            #endregion

            return CreateUserResult.Success;
        }

        public async Task<DeleteUserResult> DeleteUserAsync(int UserId)
        {
            var User = await UserRepository.GetByIdAsync(UserId);
            if (User == null)
                return DeleteUserResult.UserNotFound;

            #region Deleting Avatar
            if (User.Avatar != null)
                User.Avatar.DeleteImage(SavingPath.AvatarPath);
            #endregion


            User.IsDeleted = true;
            await UserRepository.SaveChangeAsync();
            return DeleteUserResult.Success;
        }

        public async Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter)
        => await UserRepository.FilteUsersAsync(filter);

        public async Task<UserClientSideView?> GetUserClientSideAsync(int UserId)
        {
            UserClientSideView? user=await UserRepository.GetUserForViewClientSide(UserId);
            if (user == null)
                return null;

            return user;
        }

        public async Task<UpdateUserViewModel> GetUserForEdit(int UserId)
        {
            var User = await UserRepository.GetByIdAsync(UserId);
            if (User == null)
                return null;
            return new UpdateUserViewModel()
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                city = User.city,
                PhoneNumber = User.PhoneNumber,
                NationalCode = User.NationalCode,
                BirthDay = User.BirthDay,
                email = User.email,
                Gender = User.Gender,
                Situation = User.Situation,
                Avatar = User.Avatar,
                RolesId=await userRoleRepository.GetUserRoleIdsAsync(UserId)
            };
        }

        public async Task<List<UserViewModel>?> ListUsersAsync()
        => await UserRepository.GetAllUsersAsync();

        public async Task<UpdateUserResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            var User = await UserRepository.GetByIdAsync(model.Id);
            if (User == null)
                return UpdateUserResult.UserNotFound;
            if (await UserRepository.ExistNationalCodeAsync(model.NationalCode))
                return UpdateUserResult.NationalCodeDuplicated;

            if (await UserRepository.ExistMobileAsync(model.PhoneNumber))
                return UpdateUserResult.MobileDuplicated;

            #region Update User
                User.FirstName = model.FirstName;
                User.LastName = model.LastName;
                User.city = model.city;
                User.NationalCode = model.NationalCode;
                User.Password = model.Password.EncodePasswordMd5();
                User.PhoneNumber = model.PhoneNumber;
                User.email = model.email;
                User.BirthDay = model.BirthDay;
                User.Gender = model.Gender;
                User.Situation = model.Situation;
                User.Status = UserStatus.Active;
            UserRepository.Update(User);

            #region Update Avatar
            if (model.NewImage != null)
            {
                if(User.Avatar != null)
                {
                    User.Avatar.DeleteImage(SavingPath.AvatarPath);
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.AvatarPath);
                User.Avatar = imageName;
            }
            #endregion
            await UserRepository.SaveChangeAsync();
            #region Update User Roles
            var list=await userRoleRepository.GetUserRolesIdentityKeyAsync(model.Id);
            if (list != null)
            {
                foreach (var role in list)
                {
                    userRoleRepository.Remove(new UserRole
                    {
                        UserRoleId = role
                    });
                }
                await userRoleRepository.SaveChangeAsync();
            }
            
            #region Add User Roles
            foreach (var role in model.RolesId)
            {
                await userRoleRepository.InserAsync(new UserRole { RoleId = role, UserId = User.Id });
            }
            await userRoleRepository.SaveChangeAsync();
            #endregion
            
            #endregion

            #endregion

            return UpdateUserResult.Success;
        }

    }
}
