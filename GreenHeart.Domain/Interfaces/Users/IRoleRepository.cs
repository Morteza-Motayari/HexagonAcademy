using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Users
{
    public interface IRoleRepository:IGenericRepository<Role>
    {
        Task<bool> ExistRoleTitle(string roleTilte);
        Task<bool> ExistRoleTitle(string roleTilte,int roleId);
        Task<List<RoleViewModel>> GetAllRolesOptionAsync();
        Task<FilterRoleViewModel> FilteRolesAsync(FilterRoleViewModel filter);
        Task<List<Role>?> GetUserRoles(List<int>? ids);
        Task<List<Role>?> getCaderRoles(int userId, int caderId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
