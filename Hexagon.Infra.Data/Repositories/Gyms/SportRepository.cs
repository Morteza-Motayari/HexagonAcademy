using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.Gyms;
using Hexagon.Domain.ViewModels.Gyms.SportClasses;
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
        => await _db.Sports.AnyAsync(s => s.Title == title&& s.IsDeleted==false);

        public async Task<bool> ExistSportTitle(string title, int id)
        => await _db.Sports.AnyAsync(s => s.Title == title&& s.Id!=id && s.IsDeleted == false);

        public async Task<FilterSportViewModel> FilterSportAsync(FilterSportViewModel filter)
        {
            var query = _db.Sports.Include(x => x.Certificate).AsQueryable();

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
            if (filter.Certificate != null)
            {
                query = query.Where(u => u.Certificate.Name.Contains(filter.Certificate));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new SportViewModel
            {
                Id = u.Id,
                Title = u.Title,
                CertificateId = u.CertificateId,
                Certificate=u.Certificate.Name,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted
            }));
            return filter;
        }

        public async Task<List<SportViewModel>?> GetAllSportsItemsAsync()
        => await _db.Sports.Where(s=>s.IsDeleted==false).Select(s=>new SportViewModel
        {
            Id=s.Id,
            Title=s.Title
        }).ToListAsync();

        public async Task<List<SportViewModel>?> GetAllSportsAsync()
        => await _db.Sports.Select(u => new SportViewModel
        {
            Id = u.Id,
            Title = u.Title,
            CertificateId = u.CertificateId,
            CreatedDate = u.CreatedDate,
            IsDeleted = u.IsDeleted
        }).ToListAsync();

        public async Task<int> GetSportCertifiacetId(int sportId)
        => await _db.Sports.Where(s=>s.Id==sportId).Select(s=>s.CertificateId).FirstAsync();

        public string GetSportTitle(int sportId)
        =>_db.Sports.Where(s=>s.Id==sportId).Select(s=>s.Title).First();

        public async Task<Sport?> GetSportWithCertificate(int id)
        => await _db.Sports.Include(s=>s.Certificate).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<List<ClientSideSportExisted>> GetSportExistedWithRelationAsync()
        => await _db.Sports.Include(s => s.Classes).Where(s=>!s.IsDeleted).Select(s => new ClientSideSportExisted
        {
            Id = s.Id,
            Title = s.Title,
            Slug = s.Slug,
            Classes = s.Classes.Where(c => !c.IsDeleted && c.ClassStatus != SportClassStatus.NotActive).Count()
        }).ToListAsync();
    }
}
