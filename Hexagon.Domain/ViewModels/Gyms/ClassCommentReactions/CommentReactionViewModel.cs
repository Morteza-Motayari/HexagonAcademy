using Hexagon.Domain.Enums.SportClasses;

namespace Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions
{
    public class CommentReactionViewModel
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int? UsreId { get; set; }
        public int ClassId { get; set; }
        public ClassCommentReactionType CommentReaction { get; set; }
    }
}
