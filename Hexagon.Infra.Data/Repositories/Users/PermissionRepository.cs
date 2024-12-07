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
    public class PermissionRepository:GenericRepository<Permission>,IPermissionRepository
    {
        private readonly HexagonContext _db;
        public PermissionRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Permission>> GetRolePermissions(int roleId)
        => await _db.Permissions.Include(p=>p.RolePermissions).Where(p=>p.RolePermissions.Where(r=>r.RoleId == roleId).Any()).ToListAsync();
    }
}
