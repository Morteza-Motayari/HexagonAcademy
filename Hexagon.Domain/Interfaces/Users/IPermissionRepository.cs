using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Users
{
    public interface IPermissionRepository:IGenericRepository<Permission>
    {
        Task<List<Permission>> GetRolePermissions(int roleId);
    }
}
