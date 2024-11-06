using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class GymUserRepository : GenericRepository<GymUser>, IGymUserRepository
    {
        private readonly HexagonContext _db;
        public GymUserRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }
        public async Task DeleteGymUser(int id)
        {
            GymUser? gymUser = await _db.GymUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (gymUser != null)
                _db.GymUsers.Remove(gymUser);
        }

        public async Task DeleteGymUsers(int gymId)
        {
            List<GymUser>? list = await GetGymUsersAsync(gymId);
            if (list != null)
                _db.GymUsers.RemoveRange(list);
        }

        public async Task<GymUser?> GetGymUserAsync(int id)
        => await _db.GymUsers.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<List<int>?> GetGymUserIdsAsync(int gymId)
        => await _db.GymUsers.Where(u => u.GymId == gymId).Select(x => x.UserId).ToListAsync();

        public async Task<List<GymUser>?> GetGymUsersAsync(int gymId)
        => await _db.GymUsers.Where(u => u.GymId == gymId).ToListAsync();

        public async Task<List<int>?> GetGymUsersIdentityKeyAsync(int gymId)
        => await _db.GymUsers.Where(u => u.GymId == gymId).Select(x => x.Id).ToListAsync();

        public void Remove(GymUser gymUser)
        {
            _db.GymUsers.Remove(gymUser);
        }
    }
}
