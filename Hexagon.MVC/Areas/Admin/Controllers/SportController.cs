using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Application.Services.Interfaces.Records;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class SportController(ISportService sportService,ICertificateService certificateService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageSports")]
        public async Task<IActionResult> List(FilterSportViewModel filter)
        {
            var list = await sportService.FilterSportsAsync(filter);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddSport")]
        public async Task<IActionResult> Create()
        {
            ViewData["Certificates"]=await certificateService.ListCertificatesForOptionsAsync();
            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSportViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    status = 204,
                    message = ErrorMessages.InsufficintInputs
                });
            }
            #endregion
            var result = await sportService.CreateSportAsync(model);
            switch (result)
            {
                case CreateSportResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.SportAddedSuccessfully
                    });
                case CreateSportResult.DuplicatedTitle:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.SportDuplicated
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditSport")]
        public async Task<IActionResult> Edit(int id)
        {
            var Sport = await sportService.GetSportForEdit(id);
            if (Sport == null)
                return NotFound();
            if (Sport.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.SportCantbeEdited;
                return RedirectToAction("List", "Sport", new { area = "Admin" });
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            return PartialView("_Edit",Sport);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSportViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                return Ok(new
                {
                    status = 204,
                    message = ErrorMessages.InsufficintInputs
                });
            }
            #endregion
            var result = await sportService.UpdateSportAsync(model);
            switch (result)
            {
                case UpdateSportResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.SportUpdatedSuccessfully
                    });
                case UpdateSportResult.DuplicatedTitle:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.SportDuplicated
                    });
                case UpdateSportResult.SportNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.SportNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailSport")]
        public async Task<IActionResult> Detail(int id)
        {
            var Sport = await sportService.AdminSideDetailSportAsync(id);
            if (Sport == null)
                return NotFound();

            return View(Sport);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteSport")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await sportService.DeleteSportAsync(id);
            switch (result)
            {
                case DeleteSportResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportDeletedSuccessfully;
                    break;
                case DeleteSportResult.SportNotFound:
                    TempData[ErrorMessage] = ErrorMessages.SportNotFound;
                    break;
                case DeleteSportResult.SportAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.SportAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteSportForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await sportService.DeleteSportForever(id);
            switch (result)
            {
                case DeleteForeverSportResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportDeletedForeverSuccessfully;
                    break;
                case DeleteForeverSportResult.CantDeletedNow:
                    string message = await sportService.CantDeleteSportForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverSportResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteSport;
                    break;
                case DeleteForeverSportResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.SportNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

    }
}
