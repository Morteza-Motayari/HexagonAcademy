using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model);
        Task<UpdateUserViewModel> GetUserForEdit(int UserId);
        Task<UpdateUserResult> UpdateUserAsync(UpdateUserViewModel model);
        Task<DeleteUserResult> DeleteUserAsync(int UserId);
        Task<ReadOnlyCollection<UserViewModel>?> ListUsersForItemsAsync(string term);
        Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter);
        Task<UserClientSideView?> GetUserClientSideAsync(int UserId);
        Task<AdminSideDetailUserViewModel?> AdminSideDetailUserAsync(int userId);
        Task<AdminChagePasswordViewModel?> AdminGetUserForChangePassword(int UserId);
        Task<AdminChagePasswordResult> AdminChangeUserPasswordAsync(AdminChagePasswordViewModel model);
        Task<bool> CaderHasPermissionAsync(int UserId, string permission);
        Task<DeleteForeverUserResult> DeleteUserForever(int UserId);
        Task<string> CantDeleteUserForeverNowMessage(int UserId);
        Task<bool> IsUserInfoCompleted(int UserId);
        Task<string?>GetUserAvatarUrlAsync(int UserId);
        Task<string?> GetUserNameAsync(int userId);
    }
}
