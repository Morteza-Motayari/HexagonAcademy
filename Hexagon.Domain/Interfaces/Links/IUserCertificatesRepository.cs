using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Links
{
    public interface IUserCertificatesRepository : IGenericRepository<UserCertificates>
    {
        Task<List<UserCertificates>?> GetUserCertificatesAsync(int userId);
        Task<List<int>?> GetUserCertificateIdsAsync(int userId);
        Task<List<int>?> GetUserCertificatesIdentityKeyAsync(int userId);
        Task<UserCertificates?> GetUserCertificateAsync(int id);
        Task DeleteUserCertificate(int id);
        Task DeleteUserCertificates(int userId);
        void Remove(UserCertificates userCertificate);
        Task<List<int>?> GetStaffCertificateIdsAsync(int staffId);
        Task<List<int>?> GetStaffCertificatesIdentityKeyAsync(int staffId);
        Task<bool> ExistCertificateForUser(int userId,int certificateId, int staffId,int editStaffId);
        Task<bool> ExistCertificateForUser(int userId, int certificateId, int staffId);
    }
}
