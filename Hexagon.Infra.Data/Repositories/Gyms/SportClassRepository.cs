using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class SportClassRepository : GenericRepository<SportClass>, ISportClassRepository
    {
        private readonly HexagonContext _db;
        public SportClassRepository(HexagonContext db) : base(db)
        {
            _db = db;
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
                CreatedDate = u.CreatedDate
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
            CreatedDate = u.CreatedDate
        }).ToListAsync();

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
    }
}
