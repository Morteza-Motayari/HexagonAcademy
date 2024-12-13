using Hexagon.Domain.Enums.SportClasses;

namespace Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions
{
    public class GetCommentReactionViewModel
    {
        public int CommentId { get; set; }
        public int? UsreId { get; set; }
        public ClassCommentReactionType CommentReaction { get; set; }
    }
}
