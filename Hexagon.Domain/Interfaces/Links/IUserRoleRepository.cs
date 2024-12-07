using Hexagon.Domain.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Links
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<List<UserRole>?> GetUserRolesAsync(int userId);
        Task<List<int>?> GetUserRoleIdsAsync(int userId);
        Task<List<int>?> GetUserRolesIdentityKeyAsync(int userId);
        Task<UserRole?> GetUserRoleAsync(int id);
        Task DeleteUserRole(int id);
        Task DeleteUserRoles(int userId);
        void Remove(UserRole userRole);
        Task<bool> ExistRoleForUser(int userId, int roleId, int caderId);
        Task<bool> ExistRoleForUser(int userId, int roleId, int caderId, int editCaderId);
        Task<List<int>?> GetCaderRoleIdsAsync(int caderId);
        Task<List<int>?> GetCaderRolesIdentityKeyAsync(int caderId);
        Task<List<int>?> GetActiveUserRoleIdsAsync(int userId);

    }
}
