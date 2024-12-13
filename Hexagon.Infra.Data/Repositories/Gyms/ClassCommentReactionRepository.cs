using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassCommentReactions;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class ClassCommentReactionRepository : GenericRepository<ClassCommentReaction>, IClassCommentReactionRepository
    {
        private readonly HexagonContext _db;

        public ClassCommentReactionRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> CountCommentDislikes(int coomentId)
        =>await _db.ClassCommentReactions
            .Where(r=>r.CommentId== coomentId&&r.ReactionType==ClassCommentReactionType.Like).CountAsync();

        public async Task<int> CountCommentlikes(int coomentId)
        => await _db.ClassCommentReactions
            .Where(r => r.CommentId == coomentId && r.ReactionType == ClassCommentReactionType.DisLike).CountAsync();

        public async Task<bool> ExistCommentVoteForUser(int coomentId, int userId)
        => await _db.ClassCommentReactions.AnyAsync(v=>v.CommentId==coomentId&&v.CreatedBy==userId);

        public async Task<List<ClassCommentReaction>?> GetCommentDisLikesAsync(int commentId)
        =>await _db.ClassCommentReactions
            .Where(c=>c.CommentId == commentId&&c.ReactionType==ClassCommentReactionType.DisLike).ToListAsync();

        public async Task<List<ClassCommentReaction>?> GetCommentLikesAsync(int commentId)
        => await _db.ClassCommentReactions
            .Where(c => c.CommentId == commentId && c.ReactionType == ClassCommentReactionType.Like).ToListAsync();

        public async Task<ClassCommentReaction> GetCommentReaction(int coomentId, int userId)
        =>await _db.ClassCommentReactions.Where(r=>r.CreatedBy==userId&&r.CommentId==coomentId).FirstAsync();
    }
}
