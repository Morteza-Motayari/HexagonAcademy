using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Gyms.SportClasses;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories
{
    public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
    {
        private readonly GreenHeartContext _db;
        public CertificateRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> DupliCateCertificateName(string certificateName)
        => await _db.Certificates.AnyAsync(c=>c.Name == certificateName&&c.IsDeleted==false);

        public async Task<bool> DupliCateCertificateName(string certificateName, int id)
        => await _db.Certificates.AnyAsync(c => c.Name == certificateName &&c.Id!=id &&c.IsDeleted == false);

        public async Task<FilterCertificateViewModel> FilterCertificateAsync(FilterCertificateViewModel filter)
        {
            var query = _db.Certificates.AsQueryable();

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
                query = query.Where(r => r.Name.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new CertificateViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Details=u.Details,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate
            }));
            return filter;
        }

        public async Task<List<CertificateViewModel>?> GetAllCertificatesItemsAsync()
        => await _db.Certificates.Where(c=>c.IsDeleted==false).Select(u => new CertificateViewModel
        {
            Id = u.Id,
            Name = u.Name
        }).ToListAsync();

        public async Task<string?> GetCertificateTitle(int id)
        => await _db.Certificates.Where(s=>s.Id==id).Select(c=>c.Name).FirstAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Certificates.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<ICollection<Certificate>?> GetTrainerCertificate(int userId, int staffId)
        => await _db.Certificates.Include(c=>c.UserCertificates).Where(u=>u.UserCertificates.Where(c=>c.UserId==userId&&c.StaffId==staffId).Any()).ToListAsync();

        async Task<List<CertificateViewModel>?> GetAllCertificatesAsync()
        => await _db.Certificates.Select(u=>new CertificateViewModel
        {
            Id=u.Id,
            Name = u.Name,
            Details = u.Details,
            IsDeleted=u.IsDeleted,
            CreatedDate=u.CreatedDate
        }).ToListAsync();

    }
}
