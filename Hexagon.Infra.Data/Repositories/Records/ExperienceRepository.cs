using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Records.Experiences;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories
{
    public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
    {
        private readonly HexagonContext _db;
        public ExperienceRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExistSpecificSlug(string slug)
        => await _db.SportClasses.AnyAsync(u => u.Slug == slug);

        public async Task<FilterExperienceViewModel> FilterExperienceAsync(FilterExperienceViewModel filter, int userId)
        {
            var query = _db.Experiences.Where(u=>u.UserId==userId).AsQueryable();

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

            await filter.Paging(query.Select(u => new ExperienceViewModel
            {
                Id = u.Id,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate,
                Detail = u.Detail,
                CertificateId = u.CertificateId,
                Company = u.Company,
                Title = u.Title,
                HowLong = u.HowLong,
                UserId = userId
            }));
            return filter;
        }

        public async Task<List<ExperienceViewModel>?> GetAllExperiencesAsync(int userId)
        => await _db.Experiences.Where(e=>e.UserId == userId).Include(u=>u.certificate).Select(u=>new ExperienceViewModel
        {
            Id = u.Id,
            IsDeleted = u.IsDeleted,
            CreatedDate = u.CreatedDate,
            Detail=u.Detail,
            CertificateId=u.CertificateId,
            Company=u.Company,
            Title=u.Title,
            HowLong=u.HowLong,
            UserId=userId
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

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Experiences.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
