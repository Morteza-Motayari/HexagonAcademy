using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Links
{
    public class UserCertificatesRepository : GenericRepository<UserCertificates>, IUserCertificatesRepository
    {
        private readonly GreenHeartContext _db;
        public UserCertificatesRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteUserCertificate(int id)
        {
            UserCertificates? userCertificates = await _db.UserCertificates.FirstOrDefaultAsync(u => u.Id == id);
            if (userCertificates != null)
                _db.UserCertificates.Remove(userCertificates);
        }

        public async Task DeleteUserCertificates(int userId)
        {
            List<UserCertificates>? list = await GetUserCertificatesAsync(userId);
            if (list != null)
                _db.UserCertificates.RemoveRange(list);
        }

        public async Task<bool> ExistCertificateForUser(int userId, int certificateId, int staffId, int editStaffId)
        => await _db.UserCertificates.AnyAsync(u => u.UserId == userId && u.CertificateId == certificateId && u.StaffId == staffId&&u.StaffId!= editStaffId);
        public async Task<bool> ExistCertificateForUser(int userId, int certificateId,int staffId)
        => await _db.UserCertificates.AnyAsync(u => u.UserId == userId && u.CertificateId == certificateId&&u.StaffId==staffId);

        public async Task<List<int>?> GetStaffCertificateIdsAsync(int staffId)
        => await _db.UserCertificates.Where(u => u.StaffId == staffId).Select(x => x.CertificateId).ToListAsync();

        public async Task<List<int>?> GetStaffCertificatesIdentityKeyAsync(int staffId)
        => await _db.UserCertificates.Where(u => u.StaffId == staffId).Select(x => x.Id).ToListAsync();

        public async Task<UserCertificates?> GetUserCertificateAsync(int id)
        => await _db.UserCertificates.FirstOrDefaultAsync(u => u.Id == id);


        public async Task<List<int>?> GetUserCertificateIdsAsync(int userId)
        => await _db.UserCertificates.Where(u => u.UserId == userId).Select(x => x.CertificateId).ToListAsync();

        public async Task<List<UserCertificates>?> GetUserCertificatesAsync(int userId)
        => await _db.UserCertificates.Where(u => u.UserId == userId).ToListAsync();

        public async Task<List<int>?> GetUserCertificatesIdentityKeyAsync(int userId)
        => await _db.UserCertificates.Where(u => u.UserId == userId).Select(x => x.Id).ToListAsync();

        public void Remove(UserCertificates userCertificates)
        {
            _db.UserCertificates.Remove(userCertificates);
        }

    }
}
