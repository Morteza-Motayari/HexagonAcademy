using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Interfaces.Users
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
    }
}
