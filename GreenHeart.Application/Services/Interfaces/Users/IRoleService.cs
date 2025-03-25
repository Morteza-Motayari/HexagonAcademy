using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Users
{
    public interface IRoleService
    {
        Task<CreateRoleResult> CreateRoleAsync(CreateRoleViewModel model);
        Task<UpdateRoleViewModel> GetRoleForEdit(int RoleId);
        Task<UpdateRoleResult> UpdateRoleAsync(UpdateRoleViewModel model);
        Task<DeleteRoleResult> DeleteRoleAsync(int RoleId);
        Task<List<RoleViewModel>> ListRolesAsync();
        Task<FilterRoleViewModel> FilterRolesAsync(FilterRoleViewModel filter);
        Task<List<Permission>> GetAllPermmisions();
        Task<AdminSideDetailRoleViewModel?> AdminSideDetailRoleAsync(int roleId);
        Task<DeleteForeverRoleResult> DeleteRoleForever(int RoleId);
        Task<string> CantDeleteRoleForeverNowMessage(int RoleId);
    }
}
