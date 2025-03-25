using GreenHeart.Domain.ViewModels.Gyms.ClassCommentReactions;

namespace GreenHeart.Application.Services.Interfaces.Gyms
{
    public interface IClassCommentReactionService
    {
        Task<ClientSideUpdateCommentReactionViewModel> GetClassCommentReactionForEdit(int reactionId);
        Task<ClientSideUpdateCommentReactionResult> UpdateClassCommentReactionAsync(ClientSideUpdateCommentReactionViewModel model);
        Task<ClientSideDeleteCommentReactionResult> DeleteCommentReactionAsync(int reactionId);
        Task<int> AddCommentVoteForUserAsync(ClientSideInsertCommentReactionViewModel model);
        Task<int> CommentLikesAmount(int commentId);
        Task<int> CommentDisLikesAmount(int commentId);
    }
}
