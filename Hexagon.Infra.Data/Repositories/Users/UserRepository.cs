using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly HexagonContext _db;
        public UserRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistMobileAsync(string mobile)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile);

        public async Task<User?> GetbyMobileAndPassword(string mobile, string password)
        => await _db.Users.FirstOrDefaultAsync(u=>u.PhoneNumber == mobile && u.Password == password);

        public async Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.VerificationCode == verificationCode);

        public async Task<User?> GetByMobileAsync(string mobile)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile);

        public async Task<bool> MobileDuplicatedAsync(string mobile, int userId)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id!=userId);

    }
}
