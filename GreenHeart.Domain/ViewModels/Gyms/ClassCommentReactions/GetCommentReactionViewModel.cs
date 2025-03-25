using GreenHeart.Domain.Enums.SportClasses;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassCommentReactions
{
    public class GetCommentReactionViewModel
    {
        public int CommentId { get; set; }
        public int? UsreId { get; set; }
        public ClassCommentReactionType CommentReaction { get; set; }
    }
}
