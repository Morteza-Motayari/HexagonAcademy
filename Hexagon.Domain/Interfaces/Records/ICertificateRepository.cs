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
        Task<List<CertificateViewModel>?> GetAllCertificatesAsync();
        Task<bool> DupliCateCertificateName(string certificateName);
    }
}
