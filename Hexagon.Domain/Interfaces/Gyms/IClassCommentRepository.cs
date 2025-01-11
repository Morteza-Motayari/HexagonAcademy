using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Gyms
{
    public interface IClassCommentRepository: IGenericRepository<ClassComment>
    {
        Task<FilterCommentViewModel> FilterCommentAsync(FilterCommentViewModel filter);
        Task<List<int>> GetClassActiveCommentsIds(int classId);
        Task<int> ClassCommentAmountAsync(int classId);
        Task<int> CommentClassId(int commentId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<ClientSideFilterCommentViewModel> GetUserComments(int userId, ClientSideFilterCommentViewModel filter);
        Task<bool> ExistUserCommentForSportClass(int userId,int classId,string comment);

    }
}
