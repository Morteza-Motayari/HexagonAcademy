using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Statics;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Users.Users;
using GreenHeart.Infra.Data.Repositories.Users;
using GreenHeart.Infra.Data.Repositories;
using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class CertificateService(ICertificateRepository CertificateRepository,IUserRepository userRepository) : ICertificateService
    {
        public async Task<AdminSideDetailCertificateViewModel?> AdminSideDetailCertificateAsync(int CertificateId)
        {
            var certificate = await CertificateRepository.GetByIdAsync(CertificateId);
            if (certificate == null)
                return null;
            AdminSideDetailCertificateViewModel? Detail = new()
            {
                Id = CertificateId,
                Name = certificate.Name,
                Details= certificate.Details,
                CreatedDate = certificate.CreatedDate,
                LastModifiedDate = certificate.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(certificate.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(certificate.LastModifiedBy),                
                CreatedById = certificate.CreatedBy,
                LastModifiedById = certificate.LastModifiedBy,
                IsDeleted = certificate.IsDeleted
            };
            return Detail;
        }

        public async Task<string> CantDeleteCertificateForeverNowMessage(int CertificateId)
        {
            DateTime lastEdit = await CertificateRepository.GetLastModifiedDate(CertificateId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این مدرک تا {leftdays} روز آینده را ندارید.";
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

        public async Task<DeleteForeverCertificateResult> DeleteCertificateForever(int CertificateId)
        {
            DateTime lastDate = await CertificateRepository.GetLastModifiedDate(CertificateId);
            if (lastDate.SixMonthPassed())
            {
                var certificate = await CertificateRepository.GetByIdAsync(CertificateId);

                if (certificate == null)
                    return DeleteForeverCertificateResult.NotFound;

                if (certificate.IsDeleted == false)
                    return DeleteForeverCertificateResult.FirstDeleteSimple;

                CertificateRepository.Delete(certificate);
                await CertificateRepository.SaveChangeAsync();
                return DeleteForeverCertificateResult.Success;
            }
            else
            {
                return DeleteForeverCertificateResult.CantDeletedNow;
            }
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
