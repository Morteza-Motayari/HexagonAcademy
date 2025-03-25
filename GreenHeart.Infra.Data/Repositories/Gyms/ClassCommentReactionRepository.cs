using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.ClassCommentReactions;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Gyms
{
    public class ClassCommentReactionRepository : GenericRepository<ClassCommentReaction>, IClassCommentReactionRepository
    {
        private readonly GreenHeartContext _db;

        public ClassCommentReactionRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> CountCommentDislikes(int coomentId)
        =>await _db.ClassCommentReactions
            .Where(r=>r.CommentId== coomentId&&r.ReactionType==ClassCommentReactionType.Like).CountAsync();

        public async Task<int> CountCommentlikes(int coomentId)
        => await _db.ClassCommentReactions
            .Where(r => r.CommentId == coomentId && r.ReactionType == ClassCommentReactionType.DisLike).CountAsync();

        public async Task DeleteCommentReactions(int commentId)
        {
            var reactions=await _db.ClassCommentReactions.Where(cr=>cr.CommentId==commentId).ToListAsync();
            if (reactions.Any() && reactions != null)
            {
                foreach(var reaction in reactions)
                {
                    _db.ClassCommentReactions.Remove(reaction);
                }
                await _db.SaveChangesAsync();
            }
        }

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

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.ClassCommentReactions.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
