using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Records.Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces
{
    public interface ICertificateRepository : IGenericRepository<Certificate>
    {
        Task<FilterCertificateViewModel> FilterCertificateAsync(FilterCertificateViewModel filter);
        Task<List<CertificateViewModel>?> GetAllCertificatesItemsAsync();
        Task<bool> DupliCateCertificateName(string certificateName);
        Task<bool> DupliCateCertificateName(string certificateName, int id);
        Task<ICollection<Certificate>?> GetTrainerCertificate(int userId, int staffId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
