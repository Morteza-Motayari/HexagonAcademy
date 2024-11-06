using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Links
{
    public interface IClassUserRepository : IGenericRepository<ClassUser>
    {
        Task<List<ClassUser>?> GetClassUsersAsync(int classId);
        Task<List<int>?> GetClassUserIdsAsync(int classId);
        Task<List<int>?> GetClassUsersIdentityKeyAsync(int classId);
        Task<ClassUser?> GetClassUserAsync(int id);
        Task DeleteClassUser(int id);
        Task DeleteClassUsers(int classId);
        void Remove(ClassUser classUser);
    }
}
