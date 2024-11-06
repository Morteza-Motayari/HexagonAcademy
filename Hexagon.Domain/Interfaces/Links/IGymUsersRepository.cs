using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Links
{
    public interface IGymUserRepository : IGenericRepository<GymUser>
    {
        Task<List<GymUser>?> GetGymUsersAsync(int gymId);
        Task<List<int>?> GetGymUserIdsAsync(int gymId);
        Task<List<int>?> GetGymUsersIdentityKeyAsync(int gymId);
        Task<GymUser?> GetGymUserAsync(int id);
        Task DeleteGymUser(int id);
        Task DeleteGymUsers(int gymId);
        void Remove(GymUser gymUser);
    }
}
