using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.roles
{
    public class RolePermissionRepository:GenericRepository<RolePermission>,IRolePermissionRepository
    {
        private readonly HexagonContext _db;
        public RolePermissionRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteRolePermission(int id)
        {
            RolePermission? rolePermission=await _db.RolePermissions.FirstOrDefaultAsync(u=>u.Id==id);
            if(rolePermission != null)
            _db.RolePermissions.Remove(rolePermission);
        }

        public async Task DeleteRolePermissions(int roleId)
        {
            List<RolePermission>? list=await GetRolePermissionsAsync(roleId);
            if (list != null)
            {
             _db.RolePermissions.RemoveRange(list);
            }
            
        }
        public async Task<RolePermission?> GetRolePermissionAsync(int id)
        =>await _db.RolePermissions.FirstOrDefaultAsync(u=>u.Id==id);

        public async Task<List<int>?> GetRolePermissionIdsAsync(int roleId)
        => await _db.RolePermissions.Where(u=>u.RoleId==roleId).Select(x=>x.PermissionId).ToListAsync();

        public async Task<List<RolePermission>?> GetRolePermissionsAsync(int roleId)
        => await _db.RolePermissions.Where(u=>u.RoleId==roleId).ToListAsync();

        public async Task<List<int>?> GetRolePermissionsIdentityKeyAsync(int roleId)
        => await _db.RolePermissions.Where(u => u.RoleId == roleId).Select(x => x.Id).ToListAsync();

        public void Remove(RolePermission RolePermission)
        {
            _db.RolePermissions.Remove(RolePermission);
        }
    }
}
