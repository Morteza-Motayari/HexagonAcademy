using Hexagon.Application.Extensions;
using Hexagon.Application.Generators;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Statics;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Repositories.Users;
using Hexagon.Infra.Data.Repositories;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class CertificateService(ICertificateRepository CertificateRepository,IUserRepository userRepository) : ICertificateService
    {
        public async Task<AdminSideDetailCertificateViewModel?> AdminSideDetailCertificateAsync(int CertificateId)
        {
            var user = await CertificateRepository.GetByIdAsync(CertificateId);
            if (user == null)
                return null;
            AdminSideDetailCertificateViewModel? Detail = new()
            {
                Id = CertificateId,
                Name = user.Name,
                Details=user.Details,
                CreatedDate = user.CreatedDate,
                LastModifiedDate = user.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(user.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(user.LastModifiedBy),                
                CreatedById = user.CreatedBy,
                LastModifiedById = user.LastModifiedBy,
                IsDeleted = user.IsDeleted
            };
            return Detail;
        }

        public async Task<CreateCertificateResult> CreateCertificateAsync(CreateCertificateViewModel model)
        {
            if(await CertificateRepository.DupliCateCertificateName(model.Name))
                return CreateCertificateResult.DuplicatedCertificate;
            Certificate certificate = new()
            {
                CreatedDate = DateTime.Now,
                Name = model.Name,
                Details = model.Details
            };
            await CertificateRepository.InserAsync(certificate);
            await CertificateRepository.SaveChangeAsync();
            return CreateCertificateResult.Success;
        }

        public async Task<DeleteCertificateResult> DeleteCertificateAsync(int CertificateId)
        {
            var certificate = await CertificateRepository.GetByIdAsync(CertificateId);
            if (certificate == null)
                return DeleteCertificateResult.CertificateNotFound;
            if(certificate.IsDeleted==true)
                return DeleteCertificateResult.CertificateAlreadyDeleted;

            certificate.IsDeleted = true;
            CertificateRepository.Update(certificate);
            await CertificateRepository.SaveChangeAsync();
            return DeleteCertificateResult.Success;
        }

        public async Task<FilterCertificateViewModel> FilterCertificateesAsync(FilterCertificateViewModel filter)
        => await CertificateRepository.FilterCertificateAsync(filter);

        public async Task<UpdateCertificateViewModel> GetCertificateForEdit(int CertificateId)
        {
            var certificate = await CertificateRepository.GetByIdAsync(CertificateId);
            if (certificate == null)
                return null;
            return new UpdateCertificateViewModel()
            {
                Id = certificate.Id,
                Name = certificate.Name,
                Details = certificate.Details,
                IsDeleted = certificate.IsDeleted
            };
        }

        public async Task<List<CertificateViewModel>?> ListCertificatesForOptionsAsync()
        => await CertificateRepository.GetAllCertificatesItemsAsync();

        public async Task<UpdateCertificateResult> UpdateCertificateAsync(UpdateCertificateViewModel model)
        {
            var certificate = await CertificateRepository.GetByIdAsync(model.Id);
            if (certificate == null)
                return UpdateCertificateResult.CertificateNotFound;
            if (await CertificateRepository.DupliCateCertificateName(model.Name,model.Id))
                return UpdateCertificateResult.DuplicatedCertificate;
            #region Update Certificate
            certificate.Name = model.Name;
            certificate.Details = model.Details;
            CertificateRepository.Update(certificate);
            await CertificateRepository.SaveChangeAsync();
            #endregion

            return UpdateCertificateResult.Success;
        }

    }
}
