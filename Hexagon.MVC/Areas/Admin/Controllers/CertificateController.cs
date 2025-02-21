using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class CertificateController(ICertificateService certificateService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageCertificates")]
        public async Task<IActionResult> List(FilterCertificateViewModel filter)
        {
            ViewData["Title"] = Titles.AdminCertificates;
            var list = await certificateService.FilterCertificateesAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddCertificate")]
        public IActionResult Create()
        {
            ViewData["Title"] = Titles.AdminCreateCertificate;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCertificateViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminCreateCertificate;
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
            ViewData["Title"] = Titles.AdminCreateCertificate;
            return View(model);
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditCertificate")]
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
            ViewData["Title"] = Titles.AdminEditCertificate;
            return View(certificate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCertificateViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = Titles.AdminEditCertificate;
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
            ViewData["Title"] = Titles.AdminEditCertificate;
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailCertificate")]
        public async Task<IActionResult> Detail(int id)
        {
            var certificate = await certificateService.AdminSideDetailCertificateAsync(id);
            if (certificate == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailCertificate;
            return View(certificate);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteCertificate")]
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

        #region Delete Forever
        [AuthorizePermission("DeleteCertificateForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await certificateService.DeleteCertificateForever(id);
            switch (result)
            {
                case DeleteForeverCertificateResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CertificateDeletedForeverSuccessfully;
                    break;
                case DeleteForeverCertificateResult.CantDeletedNow:
                    string message = await certificateService.CantDeleteCertificateForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverCertificateResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteCertificate;
                    break;
                case DeleteForeverCertificateResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.CertificateNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
