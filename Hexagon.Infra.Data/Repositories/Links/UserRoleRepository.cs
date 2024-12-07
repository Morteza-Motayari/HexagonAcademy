using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly HexagonContext _db;
        public UserRoleRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteUserRole(int id)
        {
            UserRole? userRole = await _db.userRoles.FirstOrDefaultAsync(u => u.UserRoleId == id);
            if (userRole != null)
                _db.userRoles.Remove(userRole);
        }

        public async Task DeleteUserRoles(int userId)
        {
            List<UserRole>? list = await GetUserRolesAsync(userId);
            if (list != null)
                _db.userRoles.RemoveRange(list);
        }

        public async Task<bool> ExistRoleForUser(int userId, int roleId, int caderId)
        => await _db.userRoles.AnyAsync(u => u.UserId == userId && u.RoleId == roleId && u.CaderId == caderId);

        public async Task<bool> ExistRoleForUser(int userId, int roleId, int caderId, int editCaderId)
        => await _db.userRoles.AnyAsync(u => u.UserId == userId && u.RoleId == roleId && u.CaderId == caderId&&u.CaderId!=editCaderId);

        public async Task<List<int>?> GetCaderRoleIdsAsync(int caderId)
        => await _db.userRoles.Where(r => r.CaderId == caderId).Select(a => a.RoleId).ToListAsync();

        public async Task<List<int>?> GetCaderRolesIdentityKeyAsync(int caderId)
        => await _db.userRoles.Where(r=>r.CaderId==caderId).Select(c=>c.UserRoleId).ToListAsync();

        public async Task<UserRole?> GetUserRoleAsync(int id)
        => await _db.userRoles.FirstOrDefaultAsync(u => u.UserRoleId == id);

        public async Task<List<int>?> GetUserRoleIdsAsync(int userId)
        => await _db.userRoles.Where(u => u.UserId == userId).Select(x => x.RoleId).ToListAsync();
        public async Task<List<int>?> GetActiveUserRoleIdsAsync(int userId)
        => await _db.userRoles.Include(u=>u.Cader).Where(u => u.UserId == userId&&!u.Cader.IsDeleted).Select(x => x.RoleId).ToListAsync();
        public async Task<List<UserRole>?> GetUserRolesAsync(int userId)
        => await _db.userRoles.Where(u => u.UserId == userId).ToListAsync();

        public async Task<List<int>?> GetUserRolesIdentityKeyAsync(int userId)
        => await _db.userRoles.Where(u => u.UserId == userId).Select(x => x.UserRoleId).ToListAsync();

        public void Remove(UserRole userRole)
        {
            _db.userRoles.Remove(userRole);
        }
    }
}
