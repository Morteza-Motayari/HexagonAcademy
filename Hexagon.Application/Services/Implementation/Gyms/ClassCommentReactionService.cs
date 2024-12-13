using Hexagon.Application.Services.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions;

namespace Hexagon.Application.Services.Implementation.Gyms
{
    public class ClassCommentReactionService(IClassCommentReactionRepository classCommentReactionRepository
        ,IClassCommentRepository classCommentRepository) : IClassCommentReactionService
    {
        public async Task<int> AddCommentVoteForUserAsync(ClientSideInsertCommentReactionViewModel model)
        {
            var voteExist = await classCommentReactionRepository.ExistCommentVoteForUser(model.commentId, model.userId);
            if (voteExist == false)
            {
                int classId=await classCommentRepository.CommentClassId(model.commentId);
                ClassCommentReaction commentReaction = new()
                {
                    ReactionType = model.CommentReaction,
                    CommentId = model.commentId,
                    SportClassId = classId,
                };

                await classCommentReactionRepository.InserAsync(commentReaction);
                await classCommentReactionRepository.SaveChangeAsync();
                return commentReaction.Id;
            }
            else
            {
                var vote=await classCommentReactionRepository.GetCommentReaction(model.commentId,model.userId);
                vote.ReactionType = model.CommentReaction;
                classCommentReactionRepository.Update(vote);
                await classCommentReactionRepository.SaveChangeAsync();
                return vote.Id;
            }
        }

        public async Task<int> CommentDisLikesAmount(int commentId)
        =>await classCommentReactionRepository.CountCommentlikes(commentId);

        public async Task<int> CommentLikesAmount(int commentId)
        => await classCommentReactionRepository.CountCommentDislikes(commentId);

        public async Task<ClientSideDeleteCommentReactionResult> DeleteCommentReactionAsync(int reactionId)
        {
            var commentReaction=await classCommentReactionRepository.GetByIdAsync(reactionId);
            if (commentReaction == null)
                return ClientSideDeleteCommentReactionResult.ClassCommentReactionNotFound;
            classCommentReactionRepository.Delete(commentReaction);
            await classCommentReactionRepository.SaveChangeAsync();
            return ClientSideDeleteCommentReactionResult.Success;

        }

        public async Task<ClientSideUpdateCommentReactionViewModel> GetClassCommentReactionForEdit(int reactionId)
        {
            var commentReaction = await classCommentReactionRepository.GetByIdAsync(reactionId);
            if (commentReaction == null)
                return null;
            return new ClientSideUpdateCommentReactionViewModel()
            {
                Id = commentReaction.Id,
                UserId=commentReaction.CreatedBy,
                ClassId=commentReaction.SportClassId,
                CommentId=commentReaction.Id,
                CommentReaction=commentReaction.ReactionType
            };
        }

        public async Task<ClientSideUpdateCommentReactionResult> UpdateClassCommentReactionAsync(ClientSideUpdateCommentReactionViewModel model)
        {
            var commentReaction = await classCommentReactionRepository.GetByIdAsync(model.Id);
            if (commentReaction == null)
                return ClientSideUpdateCommentReactionResult.ClassCommentReactionNotFound;

            #region Update Comment Reaction
            commentReaction.ReactionType = model.CommentReaction;

            classCommentReactionRepository.Update(commentReaction);
            await classCommentReactionRepository.SaveChangeAsync();
            #endregion

            return ClientSideUpdateCommentReactionResult.Success;
        }
    }
}
