using Hexagon.Domain.ViewModels.Records.Certificates;

namespace Hexagon.Application.Services.Interfaces.Records
{
    public interface ICertificateService
    {
        Task<CreateCertificateResult> CreateCertificateAsync(CreateCertificateViewModel model);
        Task<UpdateCertificateViewModel> GetCertificateForEdit(int CertificateId);
        Task<UpdateCertificateResult> UpdateCertificateAsync(UpdateCertificateViewModel model);
        Task<DeleteCertificateResult> DeleteCertificateAsync(int CertificateId);
        Task<List<CertificateViewModel>?> ListCertificateesAsync();
        Task<FilterCertificateViewModel> FilterCertificateesAsync(FilterCertificateViewModel filter);
        Task<AdminSideDetailCertificateViewModel?> AdminSideDetailCertificateAsync(int CertificateId);

    }
}
