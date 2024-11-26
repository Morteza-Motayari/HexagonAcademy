using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model);
        Task<UpdateUserViewModel> GetUserForEdit(int UserId);
        Task<UpdateUserResult> UpdateUserAsync(UpdateUserViewModel model);
        Task<DeleteUserResult> DeleteUserAsync(int UserId);
        Task<List<UserViewModel>?> ListUsersAsync();
        Task<FilterUserViewModel> FilterUsersAsync(FilterUserViewModel filter);
        Task<UserClientSideView?> GetUserClientSideAsync(int UserId);
        Task<AdminSideDetailUserViewModel?> AdminSideDetailUserAsync(int userId);
        Task<AdminChagePasswordViewModel?> AdminGetUserForChangePassword(int UserId);
        Task<AdminChagePasswordResult> AdminChangeUserPasswordAsync(AdminChagePasswordViewModel model);
    }
}
