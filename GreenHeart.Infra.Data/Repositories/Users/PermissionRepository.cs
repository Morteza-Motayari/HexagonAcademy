using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Users
{
    public class PermissionRepository:GenericRepository<Permission>,IPermissionRepository
    {
        private readonly GreenHeartContext _db;
        public PermissionRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Permission>> GetRolePermissions(int roleId)
        => await _db.Permissions.Include(p=>p.RolePermissions).Where(p=>p.RolePermissions.Where(r=>r.RoleId == roleId).Any()).ToListAsync();
    }
}
