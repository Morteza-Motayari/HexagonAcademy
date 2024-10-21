using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> ExistMobileAsync(string mobile);
        Task<bool> MobileDuplicatedAsync(string mobile, int userId);
        Task<User?> GetbyMobileAndPassword(string mobile, string password);
        Task<User?> GetByMobileAsync(string mobile);
        Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode);
    }
}
