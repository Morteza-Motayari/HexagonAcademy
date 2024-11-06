using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class ClassUserRepository : GenericRepository<ClassUser>, IClassUserRepository
    {
        private readonly HexagonContext _db;
        public ClassUserRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }
        public async Task DeleteClassUser(int id)
        {
            ClassUser? classUser = await _db.ClassUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (classUser != null)
                _db.ClassUsers.Remove(classUser);
        }

        public async Task DeleteClassUsers(int classId)
        {
            List<ClassUser>? list = await GetClassUsersAsync(classId);
            if (list != null)
                _db.ClassUsers.RemoveRange(list);
        }

        public async Task<ClassUser?> GetClassUserAsync(int id)
        => await _db.ClassUsers.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<List<int>?> GetClassUserIdsAsync(int classId)
        => await _db.ClassUsers.Where(u => u.SportClassId == classId).Select(x => x.UserId).ToListAsync();

        public async Task<List<ClassUser>?> GetClassUsersAsync(int classId)
        => await _db.ClassUsers.Where(u => u.SportClassId == classId).ToListAsync();

        public async Task<List<int>?> GetClassUsersIdentityKeyAsync(int classId)
        => await _db.ClassUsers.Where(u => u.SportClassId == classId).Select(x => x.Id).ToListAsync();

        public void Remove(ClassUser ClassUser)
        {
            _db.ClassUsers.Remove(ClassUser);
        }

    }
}
