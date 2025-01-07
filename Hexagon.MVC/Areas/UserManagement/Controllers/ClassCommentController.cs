using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
{
    public class ClassCommentController(IClassCommentService classCommentService) : UserManagementBaseSideController
    {
        #region List
        public async Task<IActionResult> List(ClientSideFilterCommentViewModel filter)
        {
            filter.TakeEntity = 6;
            var comments = await classCommentService.GetUserCommentsAsync(User.GetUserId(), filter);
            return View(comments);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var comment = await classCommentService.GetClassCommentForEdit(id);
            return PartialView("_EditComment", comment);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClientSideUpdateCommentViewModel model)
        {
            var result = await classCommentService.UpdateClassCommentAsync(model);
            switch (result)
            {
                case ClientSideUpdateCommentResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.CommentEditedSuccessfully
                    });
                case ClientSideUpdateCommentResult.ClassCommentNotFound:
                    return Ok(new
                    {
                        status = 409,
                        message = ErrorMessages.CommentNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Delete Forever
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await classCommentService.ClientSideDeleteCommentAsync(id);
            switch (result)
            {
                case ClientSideDeleteForeverCommentResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CommentDeletedSuccessfully;
                    break;
                case ClientSideDeleteForeverCommentResult.NotFound:
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

    }
}
