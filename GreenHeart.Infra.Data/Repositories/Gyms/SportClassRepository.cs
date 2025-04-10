using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Gyms
{
    public class SportClassRepository : GenericRepository<SportClass>, ISportClassRepository
    {
        private readonly GreenHeartContext _db;
        private readonly IGymRepository _gymRepository;
        private readonly ISportRepository _sportRepository;

        public SportClassRepository(GreenHeartContext db,IGymRepository gymRepository,ISportRepository sportRepository) : base(db)
        {
            _db = db;
            _gymRepository = gymRepository;
            _sportRepository = sportRepository;
        }

        public async Task<ClientSideFilterSportClassViewModel> ClientSideFilterClasses(ClientSideFilterSportClassViewModel filter)

        {
            var query = _db.SportClasses.Include(s=>s.sport).Include(s=>s.KeyWords).Where(s=>!s.IsDeleted&&s.ClassStatus==SportClassStatus.Active).AsQueryable();

            #region Filter Search
            if (filter.Gender != null)
            {
                switch (filter.Gender)
                {
                    case FilterUserGender.All:
                        break;
                    case FilterUserGender.Male:
                        query = query.Where(u => u.Gender == UserGender.Male);
                        break;
                    case FilterUserGender.Female:
                        query = query.Where(u => u.Gender == UserGender.Female);
                        break;
                }
            }

            if (filter.SportId.HasValue)
            {
                query = query.Where(s => s.SportId == filter.SportId);
            }
            if (filter.SportSlug!=null)
            {
                query = query.Where(s => s.sport.Slug == filter.SportSlug);
            }
            if (filter.GymId.HasValue)
            {
                query = query.Where(s => s.GymId == filter.GymId);
            }
            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }
            if (filter.KeyWord != null)
            {
                query = query.Where(r => r.KeyWords.Any(k=>k.Key==filter.KeyWord));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ClientSideSportClassViewModel
            {
                slug = u.Slug,
                Title = u.Title,
                StartTime = u.StartTime,
                EndTime = u.EndTime,
                ImageUrl = u.ImageUrl,
                Gender = u.Gender
            }));
            return filter;
        }

        public async Task<bool> ExistSpecificSlug(string slug)
        => await _db.SportClasses.AnyAsync(u => u.Slug == slug);

        public async Task<FilterSportClassViewModel> FilterSportClassAsync(FilterSportClassViewModel filter)
        {
            var query = _db.SportClasses.AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case ExistingStatus.All:
                    break;
                case ExistingStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case ExistingStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }
            if (filter.Gender != null)
            {
                switch (filter.Gender)
                {
                    case FilterUserGender.All:
                        break;
                    case FilterUserGender.Male:
                        query = query.Where(u => u.Gender == UserGender.Male);
                        break;
                    case FilterUserGender.Female:
                        query = query.Where(u => u.Gender == UserGender.Female);
                        break;
                }
            }
            if (filter.ClassStatus != null)
            {
                switch (filter.ClassStatus)
                {
                    case FilterSportClassStatus.All:
                        break;
                    case FilterSportClassStatus.Active:
                        query = query.Where(u => u.ClassStatus == SportClassStatus.Active);
                        break;
                    case FilterSportClassStatus.NotActive:
                        query = query.Where(u => u.ClassStatus == SportClassStatus.NotActive);
                        break;
                    case FilterSportClassStatus.Closed:
                        query = query.Where(u => u.ClassStatus == SportClassStatus.Closed);
                        break;
                }
            }
            if (filter.SportId.HasValue)
            {
                query = query.Where(s=>s.SportId == filter.SportId);
            }
            if (filter.GymId.HasValue)
            {
                query = query.Where(s => s.GymId == filter.GymId);
            }
            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new SportClassViewModel
            {
                Id = u.Id,
                SubscriptionFee=u.SubscriptionFee,
                TrainerId=u.TrainerId,
                StartDate=u.StartDate,
                EndTime=u.EndTime,
                StartTime=u.StartTime,
                GymId=u.GymId,
                SportId=u.SportId,
                Title=u.Title,
                ImageUrl = u.ImageUrl,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate,
                MaxSubscription=u.MaxSubscription,
                gym=_gymRepository.GetGymName(u.GymId),
                sport=_sportRepository.GetSportTitle(u.SportId),
                Gender=u.Gender,
                ClassStatus=u.ClassStatus
            }));
            return filter;
        }

        public async Task<List<SportClassViewModel>?> GetAllSportClasssAsync()
        => await _db.SportClasses.Select(u=>new SportClassViewModel
        {
            Id = u.Id,
            SubscriptionFee = u.SubscriptionFee,
            TrainerId = u.TrainerId,
            StartDate = u.StartDate,
            EndTime = u.EndTime,
            StartTime = u.StartTime,
            GymId = u.GymId,
            SportId = u.SportId,
            Title = u.Title,
            ImageUrl = u.ImageUrl,
            IsDeleted = u.IsDeleted,
            CreatedDate = u.CreatedDate,
            MaxSubscription=u.MaxSubscription
        }).ToListAsync();

        public async Task<SportClass?> GetClassBySlugAsync(string slug)
        =>await _db.SportClasses.Include(s=>s.sport).Where(s=>s.Slug==slug).FirstOrDefaultAsync();

        public async Task<int> GetMaxClassAthleteSpace(int sportClassId)
        => await _db.SportClasses.Where(c=>c.Id == sportClassId).Select(s=>s.MaxSubscription).FirstAsync();

        public async Task<string?> getSportClassName(int sportClassId)
        =>await _db.SportClasses.Where(s=>s.Id == sportClassId).Select(s=>s.Title).FirstAsync();

        public async Task<SportClass?> GetSportClassWithDetails(int sportClassId)
        => await _db.SportClasses.Include(u=>u.gym)
            .Include(u => u.Trainer).Include(u => u.sport).Include(u => u.ClassUsers)
            .FirstOrDefaultAsync(s=>s.Id == sportClassId);

        //the below method will contoll of duplicating the slug and never gonna have similar slug
        public async Task<string> PutSpecificSlug(string slug)
        {
            slug = slug + $"-{1}";
            if (await ExistSpecificSlug(slug))
            {
                int lastIndex = slug.LastIndexOf('-');
                int Count = int.Parse(slug.Substring(lastIndex + 1));
                while (await ExistSpecificSlug(slug))
                {
                    Count++;
                    slug = slug.Substring(0, lastIndex + 1) + $"{Count}";
                }
            }
            return slug;
        }

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.SportClasses.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<UserSideFilterSportClassViewModel> GetUserClassesAsync(int userId, UserSideFilterSportClassViewModel filter)
        {
            var query = _db.SportClasses.Include(sc=>sc.ClassUsers).Where(s => !s.IsDeleted&&s.ClassUsers.Where(s=>s.UserId==userId).Any()).AsQueryable();

            #region Filter Search

            if (filter.SportId.HasValue)
            {
                query = query.Where(s => s.SportId == filter.SportId);
            }
            if (filter.GymId.HasValue)
            {
                query = query.Where(s => s.GymId == filter.GymId);
            }
            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new UserSideSportClassViewModel
            {
                slug = u.Slug,
                Title = u.Title,
                StartTime = u.StartTime,
                EndTime = u.EndTime,
                ImageUrl = u.ImageUrl,
                Status=u.ClassStatus,
                ClassId=u.Id,
                Gender=u.Gender,
                SubscriptionFee=u.SubscriptionFee,
                RegistraionDate=u.ClassUsers.Where(cu=>cu.UserId==userId&&cu.SportClassId==u.Id).OrderBy(s=>s.SubscriptionDate).Select(cu=>cu.SubscriptionDate).First(),
                ExtensionDate=u.ClassUsers.Where(cu=>cu.UserId==userId&&cu.SportClassId==u.Id).OrderByDescending(s=>s.SubscriptionDate).Select(cu=>cu.SubscriptionDate).First()
            }));
            return filter;
        }

        public async Task<List<ClientSideSportClassViewModel>?> GetClassesForIndexPage(FilterUserGender filter)
        {
            var query = _db.SportClasses.Where(s => !s.IsDeleted && s.ClassStatus == SportClassStatus.Active).AsQueryable();

            switch(filter)
            {
                case FilterUserGender.All:
                    break;
                case FilterUserGender.Male:
                    query=query.Where(s=>s.Gender==UserGender.Male);
                    break;
                case FilterUserGender.Female:
                    query = query.Where(s => s.Gender == UserGender.Female);
                    break;
            }
            query.OrderBy(r => Guid.NewGuid()).Take(8);

            return query.Select(s=>new ClientSideSportClassViewModel
            {
                Title = s.Title,
                StartTime=s.StartTime,
                EndTime=s.EndTime,
                Gender=s.Gender,
                ImageUrl=s.ImageUrl,
                slug=s.Slug
            }).ToList();
        }

        public async Task<string?> GetClassSlugByIdAsync(int sportClassId)
        => await _db.SportClasses.Where(s => s.Id == sportClassId)
            .Select(sc => sc.Slug).FirstOrDefaultAsync();

        //public async Task<FilterSportClassAthleteViewModel> FilterSportClassAthlete(FilterSportClassAthleteViewModel filter)
        //{

        //    var query = _db.SportClasses.Include(sc => sc.ClassUsers).ThenInclude(u => u.user).Where(s => s.Id == filter.SportClassId).AsQueryable();

        //    #region Filter Search

        //    if (filter.AthleteName != null)
        //    {
        //        query = query.Where(s => s.ClassUsers.Where(s=>s.user.FirstName.Contains(filter.AthleteName)).FirstName.Contains(filter.AthleteName) || s.user.LastName.Contains(filter.AthleteName)).Distinct();
        //    }

        //    #endregion

        //    query = query.OrderByDescending(u => u.SubscriptionDate)/*.GroupBy(u => u.UserId).Select(f => f.())*//*.GroupBy(u => u.SportClass).Select(u => u.First())*/;

        //    await filter.Paging(query.Select(u => new SportClassAthleteViewModel
        //    {
        //        UserId = u.UserId,
        //        UserName = _db.Users.Where(s => s.Id == u.UserId).Select(d => d.FirstName + " " + d.LastName).First(),
        //        RegisteredDate = _db.ClassUsers.Where(sc => sc.UserId == u.UserId && sc.SportClassId == u.SportClass.Id).OrderBy(sc => sc.SubscriptionDate)
        //    .Select(sc => sc.SubscriptionDate).First(),
        //        ExtensionDate = u.SubscriptionDate
        //    }));
        //    return filter;
        //}
    }
}
