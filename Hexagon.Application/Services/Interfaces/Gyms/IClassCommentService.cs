using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.ClassComments;

namespace Hexagon.Application.Services.Interfaces.Gyms
{
    public interface IClassCommentService
    {
        Task<ClientSideCreateCommentResult> CreateClassCommentAsync(ClientSideCreateCommentViewModel model);
        Task<ClientSideUpdateCommentViewModel> GetClassCommentForEdit(int ClassCommentId);
        Task<ClientSideUpdateCommentResult> UpdateClassCommentAsync(ClientSideUpdateCommentViewModel model);
        Task<CommentViewModel> GetClassCommentAsync(int ClassCommentId);
        Task<CommentViewModel> GetClassCommentForViewAdminAsync(int ClassCommentId);
        Task<DeleteCommentStatusResult> DeleteClassCommentAsync(int ClassCommentId);
        Task<FilterCommentViewModel> FilterClassCommentsAsync(FilterCommentViewModel filterint);
        Task<AdminSideDetailClassCommentViewModel?> AdminSideDetailClassCommentAsync(int ClassCommentId);
        Task<List<ClientSideCommentViewModel>?> GetClientClassActiveCommentAsync(int classId);
        Task<UpdateCommentStatusResult> AcceptCommentAsync(int commentId);
        Task<UpdateCommentStatusResult> RejectCommentAsync(int commentId);



    }
}
