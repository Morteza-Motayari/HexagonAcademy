using GreenHeart.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Links
{
    public interface IRolePermissionRepository : IGenericRepository<RolePermission>
    {
        Task<List<RolePermission>?> GetRolePermissionsAsync(int roleId);
        Task<List<int>?> GetRolePermissionIdsAsync(int roleId);
        Task<List<int>?> GetRolePermissionsIdentityKeyAsync(int roleId);
        Task<RolePermission?> GetRolePermissionAsync(int id);
        Task DeleteRolePermission(int id);
        Task DeleteRolePermissions(int roleId);
        void Remove(RolePermission rolePermission);
        Task<List<int>> GetRoleIdsWithPermissionName(string permissionName);
    }
}
