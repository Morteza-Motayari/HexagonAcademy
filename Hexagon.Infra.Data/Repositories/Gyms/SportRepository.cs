using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class SportRepository : GenericRepository<Sport>, ISportRepository
    {
        private readonly HexagonContext _db;
        public SportRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistSportTitle(string title)
        => await _db.Sports.AnyAsync(s => s.Title == title);

        public async Task<FilterSportViewModel> FilterSportAsync(FilterSportViewModel filter)
        {
            var query = _db.Sports.AsQueryable();

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

            await filter.Paging(query.Select(u => new SportViewModel
            {
                Id = u.Id,
                Title = u.Title,
                CertificateId = u.CertificateId,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted
            }));
            return filter;
        }

        public async Task<List<SportViewModel>?> GetAllSportsAsync()
        => await _db.Sports.Select(u => new SportViewModel
        {
            Id = u.Id,
            Title = u.Title,
            CertificateId = u.CertificateId,
            CreatedDate = u.CreatedDate,
            IsDeleted = u.IsDeleted
        }).ToListAsync();
    }
}
