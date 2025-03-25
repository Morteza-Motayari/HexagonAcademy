using GreenHeart.Application.Services.Implementation.Gyms;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using GreenHeart.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class ExperienceController(IExperienceService experienceService
        ,ICertificateService certificateService
        ,IUserService userService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageExperiences")]
        public async Task<IActionResult> List(FilterExperienceViewModel filter)
        {
            ViewData["Title"] = Titles.AdminExperiences;
            var experiences=await experienceService.FilterExperienceesAsync(filter);
            return View(experiences);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddExperience")]
        public async Task<IActionResult> Create(int staffId,int userId)
        {
            ViewData["Experiences"]=await experienceService.ListExperienceesAsync(staffId);
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            ViewData["staffName"]=await userService.GetUserNameAsync(userId);
            ViewData["Title"] = Titles.AdminCreateExperience;
            return View(new CreateExperienceViewModel
            {
                UserId = userId,
                StaffId = staffId
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateExperienceViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Experiences"] = await experienceService.ListExperienceesAsync(model.StaffId);
                ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
                ViewData["staffName"] = await userService.GetUserNameAsync(model.UserId);
                ViewData["Title"] = Titles.AdminCreateExperience;
                return View(model);
            }
            #endregion

            var result=await experienceService.CreateExperienceAsync(model);
            switch(result)
            {
                case CreateExperienceResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ExperienceAddedSuccessfully;
                    break;
            }
            ViewData["Title"] = Titles.AdminCreateExperience;
            return RedirectToAction(nameof(Create), "Experience", new { staffId =model.StaffId, userId =model.UserId});
        }
        #endregion

        #region Edit
        [AuthorizePermission("EditExperience")]
        public async Task<IActionResult> Edit(int id)
        {
            var experience=await experienceService.GetExperienceForEdit(id);
            if (experience == null)
                return NotFound();
            if (experience.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.ExperienceCantbeEdited;
                return RedirectToAction("List", "Experience", new { area = "Admin" });
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            ViewData["staffName"] = await userService.GetUserNameAsync(experience.UserId);
            ViewData["Title"] = Titles.AdminEditExperience;
            return View(experience);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateExperienceViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
            {
                ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
                ViewData["staffName"] = await userService.GetUserNameAsync(model.UserId);
                ViewData["Title"] = Titles.AdminEditExperience;
                return View(model);
            }
            #endregion

            var result=await experienceService.UpdateExperienceAsync(model);
            switch (result)
            {
                case UpdateExperienceResult.Success:
                    TempData[SuccessMessage]=SuccessMessages.ExperienceUpdatedSuccessfully;
                    return RedirectToAction(nameof(Create), "Experience", new { staffId = model.StaffId, userId = model.UserId });
                    case UpdateExperienceResult.ExperienceNotFound:
                    TempData[ErrorMessage] = ErrorMessages.ExperienceNotFound;
                    return RedirectToAction(nameof(List), "Experience");
            }
            ViewData["Certificates"] = await certificateService.ListCertificatesForOptionsAsync();
            ViewData["staffName"] = await userService.GetUserNameAsync(model.UserId);
            ViewData["Title"] = Titles.AdminEditExperience;
            return View(model);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailExperience")]
        public async Task<IActionResult> Detail(int id)
        {
            var experience = await experienceService.AdminSideDetailExperienceAsync(id);
            if (experience == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailExperience;
            return View(experience);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteExperience")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await experienceService.DeleteExperienceAsync(id);
            switch (result)
            {
                case DeleteExperienceResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ExperienceDeletedSuccessfully;
                    break;
                case DeleteExperienceResult.ExperienceNotFound:
                    TempData[ErrorMessage] = ErrorMessages.ExperienceNotFound;
                    break;
                case DeleteExperienceResult.ExperienceAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.ExperienceAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteExperienceForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await experienceService.DeleteExperienceForever(id);
            switch (result)
            {
                case DeleteForeverExperienceResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ExperienceDeletedForeverSuccessfully;
                    break;
                case DeleteForeverExperienceResult.CantDeletedNow:
                    string message = await experienceService.CantDeleteExperienceForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverExperienceResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteExperience;
                    break;
                case DeleteForeverExperienceResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.ExperienceNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
