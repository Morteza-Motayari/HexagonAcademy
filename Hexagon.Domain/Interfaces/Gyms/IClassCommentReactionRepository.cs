using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions;

namespace Hexagon.Domain.Interfaces.Gyms
{
    public interface IClassCommentReactionRepository: IGenericRepository<ClassCommentReaction>
    {
        Task<List<ClassCommentReaction>?> GetCommentLikesAsync(int commentId);
        Task<List<ClassCommentReaction>?> GetCommentDisLikesAsync(int commentId);
        Task<bool> ExistCommentVoteForUser(int coomentId, int userId);
        Task<ClassCommentReaction> GetCommentReaction(int coomentId, int userId);
        Task<int> CountCommentlikes(int coomentId);
        Task<int> CountCommentDislikes(int coomentId);
    }
}
