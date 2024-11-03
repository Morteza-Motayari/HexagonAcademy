using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Users
{
    public class UserRoleRepository:GenericRepository<UserRole>,IUserRoleRepository
    {
        private readonly HexagonContext _db;
        public UserRoleRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteUserRole(int id)
        {
            UserRole? userRole=await _db.userRoles.FirstOrDefaultAsync(u=>u.UserRoleId==id);
            if(userRole!=null)
            _db.userRoles.Remove(userRole);
        }

        public async Task DeleteUserRoles(int userId)
        {
            List<UserRole>? list=await GetUserRolesAsync(userId);
            if(list!=null)
            _db.userRoles.RemoveRange(list);
        }

        public async Task<UserRole?> GetUserRoleAsync(int id)
        =>await _db.userRoles.FirstOrDefaultAsync(u=>u.UserRoleId==id);

        public async Task<List<int>?> GetUserRoleIdsAsync(int userId)
        => await _db.userRoles.Where(u=>u.UserId==userId).Select(x=>x.RoleId).ToListAsync();

        public async Task<List<UserRole>?> GetUserRolesAsync(int userId)
        => await _db.userRoles.Where(u=>u.UserId==userId).ToListAsync();

        public async Task<List<int>?> GetUserRolesIdentityKeyAsync(int userId)
        => await _db.userRoles.Where(u => u.UserId == userId).Select(x => x.UserRoleId).ToListAsync();

        public void Remove(UserRole userRole)
        {
            _db.userRoles.Remove(userRole);
        }
    }
}
