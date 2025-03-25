using GreenHeart.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassCommentReactions
{
    public class ClientSideInsertCommentReactionViewModel
    {
        public int userId {  get; set; }
        public int commentId { get; set; }
        public int ClassId { get; set; }
        public ClassCommentReactionType CommentReaction { get; set; }
    }
    public enum ClientSideInsertCommentReactionResult
    {
        Success
    }
}
