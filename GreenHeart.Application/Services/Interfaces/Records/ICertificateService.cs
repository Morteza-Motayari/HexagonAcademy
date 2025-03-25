using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Users.Roles;

namespace GreenHeart.Application.Services.Interfaces.Records
{
    public interface ICertificateService
    {
        Task<CreateCertificateResult> CreateCertificateAsync(CreateCertificateViewModel model);
        Task<UpdateCertificateViewModel> GetCertificateForEdit(int CertificateId);
        Task<UpdateCertificateResult> UpdateCertificateAsync(UpdateCertificateViewModel model);
        Task<DeleteCertificateResult> DeleteCertificateAsync(int CertificateId);
        Task<List<CertificateViewModel>?> ListCertificatesForOptionsAsync();
        Task<FilterCertificateViewModel> FilterCertificateesAsync(FilterCertificateViewModel filter);
        Task<AdminSideDetailCertificateViewModel?> AdminSideDetailCertificateAsync(int CertificateId);
        Task<DeleteForeverCertificateResult> DeleteCertificateForever(int CertificateId);
        Task<string> CantDeleteCertificateForeverNowMessage(int CertificateId);

    }
}
