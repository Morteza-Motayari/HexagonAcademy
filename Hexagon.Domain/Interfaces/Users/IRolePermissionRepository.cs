using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Users
{
    public interface IRolePermissionRepository:IGenericRepository<RolePermission>
    {
        Task<List<RolePermission>?> GetRolePermissionsAsync(int roleId);
        Task<List<int>?> GetRolePermissionIdsAsync(int roleId);
        Task<List<int>?> GetRolePermissionsIdentityKeyAsync(int roleId);
        Task<RolePermission?> GetRolePermissionAsync(int id);
        Task DeleteRolePermission(int id);
        Task DeleteRolePermissions(int roleId);
        void Remove(RolePermission rolePermission);
    }
}
