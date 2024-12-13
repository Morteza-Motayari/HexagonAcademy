using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Implementation.Gyms;
using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentApiController(IClassCommentReactionService classCommentReactionService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CommentVoteCreate(int commentId, ClassCommentReactionType commentReaction, int classId)
        {

            ClientSideInsertCommentReactionViewModel model = new()
            {
                ClassId = classId,
                commentId = commentId,
                CommentReaction = commentReaction
            };
            //model.userId = User.GetUserId();
            var result = await classCommentReactionService.AddCommentVoteForUserAsync(model);

            var likeAmount = await classCommentReactionService.CommentLikesAmount(model.ClassId);
            var DislikeAmount = await classCommentReactionService.CommentDisLikesAmount(model.ClassId);

            return Ok(new { likeAmount, DislikeAmount });
        }
    }
}
