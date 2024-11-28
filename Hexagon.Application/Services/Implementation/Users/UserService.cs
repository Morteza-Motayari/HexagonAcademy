using Hexagon.Application.Convertors;
using Hexagon.Application.Extensions;
using Hexagon.Application.Security;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Statics;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Repositories;

namespace Hexagon.Application.Services.Implementation.Users
{
    public class UserService(IUserRepository UserRepository,
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository) : IUserService
    {
        public async Task<AdminChagePasswordResult> AdminChangeUserPasswordAsync(AdminChagePasswordViewModel model)
        {
            User? user = await UserRepository.GetByIdAsync(model.Id);
            if (user == null)
                return AdminChagePasswordResult.UserNotFound;
            user.Password = model.Password.EncodePasswordMd5();
            UserRepository.Update(user);
            await userRoleRepository.SaveChangeAsync();
            return AdminChagePasswordResult.Success;
        }

        public async Task<AdminChagePasswordViewModel?> AdminGetUserForChangePassword(int UserId)
        {
            User? user = await UserRepository.GetByIdAsync(UserId);
            if (user == null)
                return null;
            return new AdminChagePasswordViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<AdminSideDetailUserViewModel?> AdminSideDetailUserAsync(int userId)
        {
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
                return null;
            AdminSideDetailUserViewModel? Detail = new()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Gender = user.Gender,
                email = user.email,
                city = user.city,
                NationalCode = user.NationalCode,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Situation = user.Situation,
                Status = user.Status,
                IsDeleted = user.IsDeleted,
                CreatedById = user.CreatedBy,
                LastModifiedById = user.LastModifiedBy,
                BirthDay = user.BirthDay?.ToShamsi(),
                CreatedDate = user.CreatedDate,
                CreatedBy = await UserRepository.GetJustUserName(user.CreatedBy),
                LastModifiedBy = await UserRepository.GetJustUserName(user.LastModifiedBy),
                ModifiedDate = user.LastModifiedDate,
                Roles = await roleRepository.GetUserRoles(await userRoleRepository.GetUserRoleIdsAsync(user.Id))
            };

            return Detail;
        }

        public async Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model)
        {
            if (await UserRepository.ExistMobileAsync(model.PhoneNumber))
                return CreateUserResult.MobileDuplicated;
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
                Gender = model.Gender,
                Situation = model.Situation,
                Status = model.Status
            };
            if (model.BirthDay != null)
            {
                if (!model.BirthDay.CheckPersianDate())
                {
                    return CreateUserResult.InvalidDateTime;
                }
                User.BirthDay = model.BirthDay.ToMiladi();
            }
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
            if (model.RolesId.CheckNullability())
            {
                foreach (var role in model.RolesId)
                {
                    await userRoleRepository.InserAsync(new UserRole { RoleId = role, UserId = User.Id });
                }
                await userRoleRepository.SaveChangeAsync();
            }
            #endregion

            return CreateUserResult.Success;
        }

        public async Task<DeleteUserResult> DeleteUserAsync(int UserId)
        {
            var User = await UserRepository.GetByIdAsync(UserId);
            if (User == null)
                return DeleteUserResult.UserNotFound;
            if (User.IsDeleted == true)
                return DeleteUserResult.UserAlreadyDeleted;

            #region Deleting Avatar
            if (User.Avatar != null)
                User.Avatar.DeleteImage(SavingPath.AvatarPath);
            #endregion


            User.IsDeleted = true;
            UserRepository.Update(User);
            await UserRepository.SaveChangeAsync();
            return DeleteUserResult.Success;
        }

        public async Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter)
        => await UserRepository.FilteUsersAsync(filter);

        public async Task<UserClientSideView?> GetUserClientSideAsync(int UserId)
        {
            UserClientSideView? user = await UserRepository.GetUserForViewClientSide(UserId);
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
                BirthDay = User.BirthDay?.ToShamsi(),
                email = User.email,
                Gender = User.Gender,
                Situation = User.Situation,
                Status = User.Status,
                Avatar = User.Avatar,
                RolesId = await userRoleRepository.GetUserRoleIdsAsync(UserId),
                IsDeleted = User.IsDeleted
            };
        }

        public async Task<List<UserViewModel>?> ListUsersAsync()
        => await UserRepository.GetAllUsersAsync();

        public async Task<UpdateUserResult> UpdateUserAsync(UpdateUserViewModel model)
        {
            var User = await UserRepository.GetByIdAsync(model.Id);
            if (User == null)
                return UpdateUserResult.UserNotFound;

            if (await UserRepository.ExistMobileAsync(model.PhoneNumber, model.Id))
                return UpdateUserResult.MobileDuplicated;

            #region Update User
            User.FirstName = model.FirstName;
            User.LastName = model.LastName;
            User.city = model.city;
            User.NationalCode = model.NationalCode;
            User.PhoneNumber = model.PhoneNumber;
            User.email = model.email;
            if (model.BirthDay != null)
            {
                if (!model.BirthDay.CheckPersianDate())
                {
                    return UpdateUserResult.InvalidDateTime;
                }
                User.BirthDay = model.BirthDay.ToMiladi();
            }
            User.Gender = model.Gender;
            User.Situation = model.Situation;
            User.Status = model.Status;
            
            #region Update Avatar
            if (model.NewImage != null)
            {
                if (User.Avatar != null)
                {
                    User.Avatar.DeleteImage(SavingPath.AvatarPath);
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.AvatarPath);
                User.Avatar = imageName;
            }
            #endregion
            UserRepository.Update(User);
            await UserRepository.SaveChangeAsync();
            #region Update User Roles
            if (model.RolesId.CheckNullability())
            {
                var list = await userRoleRepository.GetUserRolesIdentityKeyAsync(model.Id);
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
            }
            #endregion

            #endregion

            return UpdateUserResult.Success;
        }

    }
}
