using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.SportClasses;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class ClassCommentRepository : GenericRepository<ClassComment>, IClassCommentRepository
    {
        private readonly HexagonContext _db;

        public ClassCommentRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> ClassCommentAmountAsync(int classId)
        => await _db.ClassComments.Where(c=>c.SportClassId == classId&&c.CommentStatus==ClassCommentPending.Accepted&&!c.IsDeleted).CountAsync();

        public async Task<int> CommentClassId(int commentId)
        => await _db.ClassComments.Where(c => c.Id == commentId).Select(u => u.SportClassId).FirstAsync();

        public async Task<FilterCommentViewModel> FilterCommentAsync(FilterCommentViewModel filter)
        {
            var query = _db.ClassComments.Include(c=>c.User).Include(s=>s.sportClass).AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case ExistingStatus.All:
                    break;
                case ExistingStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case ExistingStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }

            switch (filter.CommentStatus)
            {
                case ClassCommentPendingAdmin.All:
                    break;
                case ClassCommentPendingAdmin.CommentSent:
                    query = query.Where(u => u.CommentStatus == ClassCommentPending.CommentSent);
                    break;
                case ClassCommentPendingAdmin.Accepted:
                    query = query.Where(u => u.CommentStatus == ClassCommentPending.Accepted);
                    break;
                case ClassCommentPendingAdmin.Rejected:
                    query = query.Where(u => u.CommentStatus == ClassCommentPending.Rejected);
                    break;
            }

            if (filter.Comment != null)
            {
                query = query.Where(r => r.Comment.Contains(filter.Comment));
            }
            if (filter.UserName != null)
            {
                query = query.Where(r => r.User.FirstName.Contains(filter.UserName)|| r.User.LastName.Contains(filter.UserName)).Distinct();
            }
            if (filter.SportClass != null)
            {
                query = query.Where(r => r.sportClass.Title.Contains(filter.SportClass));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new CommentViewModel
            {
                Id = u.Id,
                Comment = u.Comment,
                ClassId = u.SportClassId,
                UserId = u.CreatedBy,
                UserName = u.User.FirstName+" "+u.User.LastName,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate,
                SportClass=u.sportClass.Title,
                CommentStatus=u.CommentStatus
            }));
            return filter;
        }

        public async Task<List<int>> GetClassActiveCommentsIds(int classId)
        => await _db.ClassComments.Where(c => !c.IsDeleted && c.CommentStatus == ClassCommentPending.Accepted&&c.SportClassId==classId)
            .Select(c=>c.Id).ToListAsync();
        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.ClassComments.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
