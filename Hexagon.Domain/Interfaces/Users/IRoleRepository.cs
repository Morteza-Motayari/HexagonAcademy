using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Users
{
    public interface IRoleRepository:IGenericRepository<Role>
    {
        Task<bool> ExistRoleTitle(string roleTilte);
        Task<List<RoleViewModel>> GetAllRolesAsync();
        Task<FilterRoleViewModel> FilteRolesAsync(FilterRoleViewModel filter);
        Task<List<Role>?> GetUserRoles(List<int>? ids);
    }
}
