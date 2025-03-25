using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Links
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly GreenHeartContext _db;
        public RolePermissionRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteRolePermission(int id)
        {
            RolePermission? rolePermission = await _db.RolePermissions.FirstOrDefaultAsync(u => u.Id == id);
            if (rolePermission != null)
                _db.RolePermissions.Remove(rolePermission);
        }

        public async Task DeleteRolePermissions(int roleId)
        {
            List<RolePermission>? list = await GetRolePermissionsAsync(roleId);
            if (list != null)
            {
                _db.RolePermissions.RemoveRange(list);
            }

        }

        public async Task<List<int>> GetRoleIdsWithPermissionName(string permissionName)
        {
            int? permissionId = _db.Permissions.First(p => p.PermissionName == permissionName).PermissionId;
            if (permissionId.HasValue)
            {
                return await _db.RolePermissions.Where(u => u.PermissionId == permissionId).Select(p => p.RoleId).ToListAsync();
            }
            return null;
        }

        public async Task<RolePermission?> GetRolePermissionAsync(int id)
        => await _db.RolePermissions.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<List<int>?> GetRolePermissionIdsAsync(int roleId)
        => await _db.RolePermissions.Where(u => u.RoleId == roleId).Select(x => x.PermissionId).ToListAsync();

        public async Task<List<RolePermission>?> GetRolePermissionsAsync(int roleId)
        => await _db.RolePermissions.Where(u => u.RoleId == roleId).ToListAsync();

        public async Task<List<int>?> GetRolePermissionsIdentityKeyAsync(int roleId)
        => await _db.RolePermissions.Where(u => u.RoleId == roleId).Select(x => x.Id).ToListAsync();

        public void Remove(RolePermission RolePermission)
        {
            _db.RolePermissions.Remove(RolePermission);
        }
    }
}
