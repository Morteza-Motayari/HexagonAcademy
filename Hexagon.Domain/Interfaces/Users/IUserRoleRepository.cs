using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Users
{
    public interface IUserRoleRepository:IGenericRepository<UserRole>
    {
        Task<List<UserRole>?> GetUserRolesAsync(int userId);
        Task<List<int>?> GetUserRoleIdsAsync(int userId);
        Task<List<int>?> GetUserRolesIdentityKeyAsync(int userId);
        Task<UserRole?> GetUserRoleAsync(int id);
        Task DeleteUserRole(int id);
        Task DeleteUserRoles(int userId);
        void Remove(UserRole userRole);
    }
}
