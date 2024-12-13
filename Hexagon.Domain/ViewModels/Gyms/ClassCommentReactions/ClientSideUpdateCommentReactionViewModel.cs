using Hexagon.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions
{
    public class ClientSideUpdateCommentReactionViewModel
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int ClassId { get; set; }
        public ClassCommentReactionType CommentReaction { get; set; }
    }
    public enum ClientSideUpdateCommentReactionResult
    {
        Success,
        ClassCommentReactionNotFound
    }
}
