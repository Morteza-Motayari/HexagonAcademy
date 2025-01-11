using Hexagon.Application.Services.Interfaces.KeyWords;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.KeyWords;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class KeyWordController(IKeyWordService KeyWordService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageKeyWords")]
        public async Task<IActionResult> List(FilterkeyWordViewModel filter,int sportClassId)
        {
            ViewData["SportClassId"] = sportClassId;
            var list = await KeyWordService.FilterKeyWordsAsync(filter, sportClassId);
            return View(list);
        }
        #endregion

        #region Create
        [AuthorizePermission("AddKeyWord")]
        public async Task<IActionResult> Create(int sportClassId)
        {
            ViewData["SportClassId"] = sportClassId;
            return PartialView("_AddKey");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatekeyWordViewModel model)
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
            var result = await KeyWordService.CreateKeyWordAsync(model);
            switch (result)
            {
                case CreatekeyWordResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.KeyWordAddedSuccessfully
                    });
                case CreatekeyWordResult.KeyDuplicated:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.KeyWordDuplicated
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
        [AuthorizePermission("EditKeyWord")]
        public async Task<IActionResult> Edit(int id)
        {
            var KeyWord = await KeyWordService.GetKeyWordForEdit(id);
            if (KeyWord == null)
                return NotFound();
            if (KeyWord.IsDeleted == true)
            {
                TempData[WarningMessage] = WarningMessages.KeyWordCantbeEdited;
                return RedirectToAction("List", "KeyWord", new { area = "Admin" , sportClassId =KeyWord.SportClassId});
            }
            return PartialView("_EditKey", KeyWord);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatekeyWordViewModel model)
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
            var result = await KeyWordService.UpdateKeyWordAsync(model);
            switch (result)
            {
                case UpdatekeyWordResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.KeyWordUpdatedSuccessfully
                    });
                case UpdatekeyWordResult.KeyDuplicated:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.KeyWordDuplicated
                    });
                case UpdatekeyWordResult.KeyWordNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.KeyWordNotFound
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
        [AuthorizePermission("DetailKeyWord")]
        public async Task<IActionResult> Detail(int id)
        {
            var KeyWord = await KeyWordService.AdminSideDetailKeyWordAsync(id);
            if (KeyWord == null)
                return NotFound();

            return View(KeyWord);
        }
        #endregion

        #region Delete
        [AuthorizePermission("DeleteKeyWord")]
        public async Task<IActionResult> Delete(int id,int ClassId)
        {
            var result = await KeyWordService.DeleteKeyWordAsync(id);
            switch (result)
            {
                case DeletekeyWordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.KeyWordDeletedSuccessfully;
                    break;
                case DeletekeyWordResult.KeyWordNotFound:
                    TempData[ErrorMessage] = ErrorMessages.KeyWordNotFound;
                    break;
                case DeletekeyWordResult.KeyWordAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.KeyWordAlreadyDeleted;
                    break;
            }
            return RedirectToAction("List", "KeyWord", new { area = "Admin", sportClassId = ClassId });
        }
        #endregion

        #region Delete Forever
        [AuthorizePermission("DeleteKeyWordForever")]
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await KeyWordService.DeleteKeyWordForever(id);
            switch (result)
            {
                case DeleteForeverkeyWordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.KeyWordDeletedForeverSuccessfully;
                    break;
                case DeleteForeverkeyWordResult.CantDeletedNow:
                    string message = await KeyWordService.CantDeleteKeyWordForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverkeyWordResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteKeyWord;
                    break;
                case DeleteForeverkeyWordResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.KeyWordNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

    }
}
