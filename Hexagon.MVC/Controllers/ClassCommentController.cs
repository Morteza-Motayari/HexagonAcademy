using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Hexagon.MVC.Controllers
{
    [Authorize]
    public class ClassCommentController(IClassCommentService classCommentService
        ,IClassCommentReactionService classCommentReactionService) : BaseSiteController
    {
        [HttpGet]
        public IActionResult CreateComment(int id)
        {
            return PartialView("_addComment",new ClientSideCreateCommentViewModel { ClassId= id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(ClientSideCreateCommentViewModel model)
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
            var result =await classCommentService.CreateClassCommentAsync(model);
            switch(result)
            {
                case ClientSideCreateCommentResult.Success:
                    return Ok(new
                    {
                        status = 200,
                        message = SuccessMessages.CommentAddedSuccessfully
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        [HttpGet]
        //[Consumes("application/json")]
        public async Task<IActionResult> CommentVoteCreate(ClientSideInsertCommentReactionViewModel model)
            {

            model.userId=User.GetUserId();
            var result=await classCommentReactionService.AddCommentVoteForUserAsync(model);

            var likeAmount=await classCommentReactionService.CommentLikesAmount(model.commentId);
            var DislikeAmount=await classCommentReactionService.CommentDisLikesAmount(model.commentId);

            return Ok(new { likeAmount, DislikeAmount });
        }

    }
}
