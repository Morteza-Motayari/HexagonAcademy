using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Implementation.KeyWords;
using Hexagon.Application.Services.Implementation.Users;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.KeyWords;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class ClassCommentController(IClassCommentService classCommentService) : AdminSideController
    {
        #region List
        //TODO adding new permissions
        //[AuthorizePermission("ManageClassComments")]
        public async Task<IActionResult> List(FilterCommentViewModel filter, int sportClassId)
        {
            ViewData["SportClassId"] = sportClassId;
            var list = await classCommentService.FilterClassCommentsAsync(filter);
            return View(list);
        }
        #endregion

        #region Get Comment
        public async Task<IActionResult> CommentView(int commentId)
        {
            var comment=await classCommentService.GetClassCommentForViewAdminAsync(commentId);
            return PartialView("_CommentView", comment);
        }
        #endregion

        #region Change Status
        public async Task<IActionResult> AcceptComment(int id)
        {
            var comment = await classCommentService.AcceptCommentAsync(id);
            switch(comment)
            {
                case UpdateCommentStatusResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.CommentAcceptedSuccessfully
                    });
                case UpdateCommentStatusResult.ClassCommentNotFound:
                    return Ok(new
                    {
                        status = 200,
                        message = ErrorMessages.CommentNotFound
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }

        public async Task<IActionResult> RejectComment(int id)
        {
            var comment = await classCommentService.RejectCommentAsync(id);
            switch (comment)
            {
                case UpdateCommentStatusResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.CommentRejectedSuccessfully
                    });
                case UpdateCommentStatusResult.ClassCommentNotFound:
                    return Ok(new
                    {
                        status = 200,
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

        #region Detail
        //[AuthorizePermission("DetailSport")]
        public async Task<IActionResult> Detail(int id)
        {
            var commeny = await classCommentService.AdminSideDetailClassCommentAsync(id);
            if (commeny == null)
                return NotFound();

            return View(commeny);
        }
        #endregion

        #region Delete
        //[AuthorizePermission("DeleteSport")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await classCommentService.DeleteClassCommentAsync(id);
            switch (result)
            {
                case DeleteCommentStatusResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.SportClassDeletedSuccessfully;
                    break;
                case DeleteCommentStatusResult.ClassCommentNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CommentNotFound;
                    break;
                case DeleteCommentStatusResult.ClassCommentAlreadyDeleted:
                    TempData[ErrorMessage] = ErrorMessages.CommentAlreadyDeleted;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Delete Forever
        public async Task<IActionResult> DeleteForever(int id)
        {
            var result = await classCommentService.DeleteClassCommentForever(id);
            switch (result)
            {
                case DeleteForeverCommentResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CommentDeletedForeverSuccessfully;
                    break;
                case DeleteForeverCommentResult.CantDeletedNow:
                    string message = await classCommentService.CantDeleteClassCommentForeverNowMessage(id);
                    TempData[ErrorMessage] = message;
                    break;
                case DeleteForeverCommentResult.FirstDeleteSimple:
                    TempData[ErrorMessage] = ErrorMessages.FirstSimpleDeleteComment;
                    break;
                case DeleteForeverCommentResult.NotFound:
                    TempData[ErrorMessage] = ErrorMessages.CommentNotFound;
                    break;
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
