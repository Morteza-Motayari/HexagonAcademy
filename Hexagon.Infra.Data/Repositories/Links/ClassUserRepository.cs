using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
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

        public async Task<UserSideFilterSportClassViewModel> GetUserClassesAsync(int userId, UserSideFilterSportClassViewModel filter)
        {
            var query = _db.ClassUsers.Include(sc => sc.SportClass).Where(s => !s.SportClass.IsDeleted && s.UserId == userId).AsQueryable();

            #region Filter Search

            if (filter.SportId.HasValue)
            {
                query = query.Where(s => s.SportClass.SportId == filter.SportId);
            }
            if (filter.GymId.HasValue)
            {
                query = query.Where(s => s.SportClass.GymId == filter.GymId);
            }
            if (filter.Title != null)
            {
                query = query.Where(r => r.SportClass.Title.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.SubscriptionDate)/*.GroupBy(u => u.SportClass).Select(u => u.First())*/;

            await filter.Paging(query.Select( u => new UserSideSportClassViewModel
            {
                slug = u.SportClass.Slug,
                Title = u.SportClass.Title,
                StartTime = u.SportClass.StartTime,
                EndTime = u.SportClass.EndTime,
                ImageUrl = u.SportClass.ImageUrl,
                Status = u.SportClass.ClassStatus,
                RegistraionDate = _db.ClassUsers.Where(sc => sc.UserId == userId && sc.SportClassId == u.SportClass.Id).OrderBy(sc => sc.SubscriptionDate)
            .Select(sc => sc.SubscriptionDate).First(),
                ExtensionDate=u.SubscriptionDate,
                ClassId=u.SportClass.Id,
                SubscriptionFee=u.SportClass.SubscriptionFee,
                Gender=u.SportClass.Gender
            }));
            return filter;
        }


        public async Task<bool> IsUserRegisteredInClass(int userId, int classId)
        => await _db.ClassUsers.Where(u=>u.UserId==userId&&u.SportClassId==classId).AnyAsync();

        public async Task<DateTime> LastUserRegistrationDateInClass(int userId, int classId)
        => await _db.ClassUsers.Where(u => u.UserId == userId && u.SportClassId == classId).OrderByDescending(c=>c.SubscriptionDate).Select(u=>u.SubscriptionDate).FirstAsync();

        public async Task<int> RegisteredUserLastMonth(int classId)
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);

            return await _db.ClassUsers
                .Where(c => c.SportClassId == classId && c.SubscriptionDate >= thirtyDaysAgo)
                .CountAsync();
        }

        public DateTime RegistraionDate(int userId, int classId)
        =>  _db.ClassUsers.Where(u => u.UserId == userId && u.SportClassId == classId).OrderBy(u => u.SubscriptionDate)
            .Select(u => u.SubscriptionDate).First();

        public void Remove(ClassUser ClassUser)
        {
            _db.ClassUsers.Remove(ClassUser);
        }

    }
}
