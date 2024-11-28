using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class CertificateController(ICertificateService certificateService) : AdminSideController
    {
        #region List
        public async Task<IActionResult> List(FilterCertificateViewModel filter)
        {
            var list = await certificateService.FilterCertificateesAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCertificateViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await certificateService.CreateCertificateAsync(model);
            switch (result)
            {
                case CreateCertificateResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CertificateAddedSuccessfully;
                    return RedirectToAction(nameof(List), "Certificate", new { area = "Admin" });
                case CreateCertificateResult.DuplicatedCertificate:
                    TempData[ErrorMessage] = ErrorMessages.CertificateDuplicated;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var certificate = await certificateService.GetCertificateForEdit(id);
            if (certificate == null)
                return NotFound();
            if (certificate.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.CertificateCantbeEdited;
                return RedirectToAction("List", "Certificate", new { area = "Admin" });
            }

            return View(certificate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCertificateViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            var result = await certificateService.UpdateCertificateAsync(model);
            switch (result)
            {
                case UpdateCertificateResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CertificateUpdatedSuccessfully;
                    return RedirectToAction(nameof(List), "Certificate", new { area = "Admin" });
                case UpdateCertificateResult.DuplicatedCertificate:
                    TempData[ErrorMessage] = ErrorMessages.CertificateDuplicated;
                    break;
                case UpdateCertificateResult.CertificateNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CertificateNotFound;
                    break;
            }
            return View(model);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            var certificate = await certificateService.AdminSideDetailCertificateAsync(id);
            if (certificate == null)
                return NotFound();

            return View(certificate);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result = await certificateService.DeleteCertificateAsync(id);
            switch (result)
            {
                case DeleteCertificateResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CertificateDeletedSuccessfully;
                    break;
                case DeleteCertificateResult.CertificateNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CertificateNotFound;
                    break;
                case DeleteCertificateResult.CertificateAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.CertificateAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

    }
}
