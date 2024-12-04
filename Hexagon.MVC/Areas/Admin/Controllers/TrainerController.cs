using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Records.Certificates;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class TrainerController(IStaffService staffService
        , IUserService userService
        , ICertificateService certificateService) : AdminSideController
    {
        #region List
        public async Task<IActionResult> List(FilterTrainerViewModel filter)
        {
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            var list = await staffService.FilterTrainersAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            //ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            List<CertificateViewModel>? list = await certificateService.ListCertificatesForOptionsAsync();
            if (!list.CheckNullability())
            {
                TempData[InfoMessage] = InfoMessages.CertificateDontExisted;
                return RedirectToAction("Create", "Certificate", new { area = "Admin" });
            }
            ViewData["Certificates"] = list;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainerViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
                return View(model);
            }
            #endregion
            var result = await staffService.CreateTrainerAsync(model);
            switch (result)
            {
                case CreateTrainerResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TrainerAddedSuccessfully;
                    return RedirectToAction(nameof(List), "Trainer", new { area = "Admin" });
                case CreateTrainerResult.DuplicatedPosition:
                    TempData[ErrorMessage] = ErrorMessages.TrainerPositionDuplicated;
                    break;
                case CreateTrainerResult.UserNotFound:
                    TempData[ErrorMessage] = ErrorMessages.UserNotFound;
                    break;
                case CreateTrainerResult.InValidSalary:
                    TempData[ErrorMessage] = ErrorMessages.InvalidSalaryInput;
                    break;
                case CreateTrainerResult.ExistCertificateForUser:
                    TempData[ErrorMessage] = ErrorMessages.ExistCertificateForUserTrainer;
                    break;
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            return View(model);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            var Trainer = await staffService.GetTrainerForEdit(id);
            if (Trainer == null)
                return NotFound();
            if (Trainer.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.TrainerCantbeEdited;
                return RedirectToAction("List", "Trainer", new { area = "Admin" });
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            return View(Trainer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTrainerViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
                return View(model);
            }
            #endregion
            var result = await staffService.UpdateTrainerAsync(model);
            switch (result)
            {
                case UpdateTrainerResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TrainerUpdatedSuccessfully;
                    return RedirectToAction(nameof(List), "Trainer", "Admin");
                case UpdateTrainerResult.DuplicatedPosition:
                    TempData[ErrorMessage] = ErrorMessages.TrainerPositionDuplicated;
                    break;
                case UpdateTrainerResult.TraninerNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TrainerNotFound;
                    break;
                case UpdateTrainerResult.InValidSalary:
                    TempData[ErrorMessage] = ErrorMessages.InvalidSalaryInput;
                    break;
                case UpdateTrainerResult.ExistCertificateForUser:
                    TempData[ErrorMessage] = ErrorMessages.ExistCertificateForUserTrainer;
                    break;
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            return View(model);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            var Trainer = await staffService.AdminSideDetailTrainerAsync(id);
            if (Trainer == null)
                return NotFound();

            return View(Trainer);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result = await staffService.DeleteTrainerAsync(id);
            switch (result)
            {
                case DeleteTrainerResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.TrainerDeletedSuccessfully;
                    break;
                case DeleteTrainerResult.TrainerNotFound:
                    TempData[ErrorMessage] = ErrorMessages.TrainerNotFound;
                    break;
                case DeleteTrainerResult.TrainerAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.TrainerAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region get Trainers For Options
        [HttpGet]
        public async Task<IActionResult> GetTrainers(UserGender genderval, int sportId)
        {
            if (sportId != 0)
            {
                var data = await staffService.ListTrainerForItemsAsync(genderval, sportId);
                if (!data.CheckNullability())
                {
                    return Ok(new
                    {
                        status = 101
                    });
                }
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }
        #endregion
    }
}
