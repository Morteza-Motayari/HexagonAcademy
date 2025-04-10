using GreenHeart.Application.Services.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.ClassComments;
using GreenHeart.Domain.ViewModels.Gyms.Sports;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenHeart.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Application.Extensions;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Infra.Data.Repositories.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Statics.Caches_Constatnt;

namespace GreenHeart.Application.Services.Implementation.Gyms
{
    public class ClassCommentService(IClassCommentRepository classCommentRepository
        ,IUserRepository userRepository
        ,ISportClassRepository sportClassRepository
        ,IClassCommentReactionRepository classCommentReactionRepository
        ,IHttpContextAccessor httpContextAccessor
        ,ICacheService cacheService) : IClassCommentService
    {
        public async Task<AdminSideDetailClassCommentViewModel?> AdminSideDetailClassCommentAsync(int ClassCommentId)
        {
            var comment = await classCommentRepository.GetByIdAsync(ClassCommentId);
            if (comment == null)
                return null;
            AdminSideDetailClassCommentViewModel? Detail = new()
            {
                Id = ClassCommentId,
                Comment= comment.Comment,
                ClassId=comment.SportClassId,
                SportClass=await sportClassRepository.getSportClassName(comment.SportClassId),                
                CreatedDate = comment.CreatedDate,
                LastModifiedDate = comment.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(comment.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(comment.LastModifiedBy),
                CreatedById = comment.CreatedBy,
                LastModifiedById = comment.LastModifiedBy,
                IsDeleted = comment.IsDeleted,
                CommentStatus = comment.CommentStatus
            };
            return Detail;
        }

        public async Task<ClientSideCreateCommentResult> CreateClassCommentAsync(ClientSideCreateCommentViewModel model)
        {
            int userId = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);
            if(await classCommentRepository.ExistUserCommentForSportClass(userId,model.ClassId,model.Comment))
            {
                return ClientSideCreateCommentResult.DuplicatedComment;
            }
            ClassComment classcomment = new()
            {
                Comment = model.Comment,
                SportClassId = model.ClassId
            };

            await classCommentRepository.InserAsync(classcomment);
            await classCommentRepository.SaveChangeAsync();
            return ClientSideCreateCommentResult.Success;
        }

        public async Task<DeleteCommentStatusResult> DeleteClassCommentAsync(int ClassCommentId)
        {
            var ClassComment = await classCommentRepository.GetByIdAsync(ClassCommentId);
            if (ClassComment == null)
                return DeleteCommentStatusResult.ClassCommentNotFound;
            if (ClassComment.IsDeleted == true)
                return DeleteCommentStatusResult.ClassCommentAlreadyDeleted;

            ClassComment.IsDeleted = true;
            classCommentRepository.Update(ClassComment);
            await classCommentRepository.SaveChangeAsync();
            #region Deleting Cached ClassComments
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.ClassComment + ClassComment.SportClassId.ToString());
            #endregion
            return DeleteCommentStatusResult.Success;
        }

        public async Task<FilterCommentViewModel> FilterClassCommentsAsync(FilterCommentViewModel filter)
        => await classCommentRepository.FilterCommentAsync(filter);

        public async Task<List<ClientSideCommentViewModel>?> GetClientClassActiveCommentAsync(int classId)
        {
            string cachekey=CacheKeys.ClassComment+classId.ToString();
            if(await cacheService.ExistsAsync(cachekey))
            {
                var cachedClassComments=await cacheService.GetListAsync<ClientSideCommentViewModel>(cachekey);
                if (cachedClassComments.CheckNullability())
                {
                    return cachedClassComments;
                }
            }
            var commentIds=await classCommentRepository.GetClassActiveCommentsIds(classId);
            if(commentIds == null)
                return null;
            List<ClientSideCommentViewModel> comments = new();
            foreach(var commentid in commentIds)
            {
                var item=await classCommentRepository.GetByIdAsync(commentid);
                comments.Add(new ClientSideCommentViewModel
                {
                    Id = commentid,
                    ClassId = classId,
                    Comment = item.Comment,
                    CreatedDate = item.CreatedDate,
                    UserName = await userRepository.GetJustUserName(item.CreatedBy),
                    Avatar=await userRepository.GetJustAvatarAsync(item.CreatedBy),
                    Like = await classCommentReactionRepository.GetCommentLikesAsync(commentid),
                    DisLike = await classCommentReactionRepository.GetCommentDisLikesAsync(commentid)
                });
            }
            await cacheService.SetListAsync(cachekey, comments,CacheDuration.SportClassCahingTime);
            return comments;
        }

        public async Task<CommentViewModel> GetClassCommentAsync(int ClassCommentId)
        {
            var comment=await classCommentRepository.GetByIdAsync(ClassCommentId);
            if (comment == null)
                return null;
            return new CommentViewModel
            {
                Id = comment.Id,
                ClassId = comment.SportClassId,
                IsDeleted = comment.IsDeleted,
                UserId = comment.CreatedBy,
                UserName = await userRepository.GetJustUserName(comment.CreatedBy)
            };
        }

        public async Task<ClientSideUpdateCommentViewModel> GetClassCommentForEdit(int ClassCommentId)
        {
            var ClassComment = await classCommentRepository.GetByIdAsync(ClassCommentId);
            if (ClassComment == null)
                return null;
            return new ClientSideUpdateCommentViewModel()
            {
                Id = ClassComment.Id,
                Comment = ClassComment.Comment,
                ClassId = ClassComment.SportClassId,
                IsDeleted = ClassComment.IsDeleted
            };
        }

        public async Task<ClientSideUpdateCommentResult> UpdateClassCommentAsync(ClientSideUpdateCommentViewModel model)
        {
            var ClassComment = await classCommentRepository.GetByIdAsync(model.Id);
            if (ClassComment == null)
                return ClientSideUpdateCommentResult.ClassCommentNotFound;

            #region Update ClassComment
            ClassComment.Comment = model.Comment;
            ClassComment.CommentStatus=ClassCommentPending.CommentSent;
            classCommentRepository.Update(ClassComment);
            await classCommentRepository.SaveChangeAsync();
            #endregion

            #region Deleting Cached ClassComments
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.ClassComment + ClassComment.SportClassId.ToString());
            #endregion

            return ClientSideUpdateCommentResult.Success;
        }

        public async Task<CommentViewModel> GetClassCommentForViewAdminAsync(int ClassCommentId)
        {
            var comment = await classCommentRepository.GetByIdAsync(ClassCommentId);
            if (comment == null)
                return null;
            return new CommentViewModel
            {
                Id = comment.Id,
                IsDeleted = comment.IsDeleted,
                UserName = await userRepository.GetJustUserName(comment.CreatedBy),
                Comment=comment.Comment,
                CreatedDate=comment.CreatedDate,
                SportClass=await sportClassRepository.getSportClassName(comment.SportClassId),
                CommentStatus=comment.CommentStatus
            };
        }

        public async Task<UpdateCommentStatusResult> AcceptCommentAsync(int commentId)
        {
            var comment=await classCommentRepository.GetByIdAsync(commentId);
            if (comment == null)
                return UpdateCommentStatusResult.ClassCommentNotFound;
            comment.CommentStatus = ClassCommentPending.Accepted;
            classCommentRepository.Update(comment);
            await classCommentRepository.SaveChangeAsync();
            #region Deleting Cached ClassComments
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.ClassComment + comment.SportClassId.ToString());
            #endregion
            return UpdateCommentStatusResult.Success;
        }

        public async Task<UpdateCommentStatusResult> RejectCommentAsync(int commentId)
        {
            var comment = await classCommentRepository.GetByIdAsync(commentId);
            if (comment == null)
                return UpdateCommentStatusResult.ClassCommentNotFound;
            comment.CommentStatus = ClassCommentPending.Rejected;
            classCommentRepository.Update(comment);
            await classCommentRepository.SaveChangeAsync();
            #region Deleting Cached ClassComments
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.ClassComment + comment.SportClassId.ToString());
            #endregion
            return UpdateCommentStatusResult.Success;
        }

        public async Task<DeleteForeverCommentResult> DeleteClassCommentForever(int ClassCommentId)
        {
            DateTime lastDate = await classCommentRepository.GetLastModifiedDate(ClassCommentId);
            if (lastDate.SixMonthPassed())
            {
                var classcomment = await classCommentRepository.GetByIdAsync(ClassCommentId);

                if (classcomment == null)
                    return DeleteForeverCommentResult.NotFound;

                if (classcomment.IsDeleted == false)
                    return DeleteForeverCommentResult.FirstDeleteSimple;

                classCommentRepository.Delete(classcomment);
                await classCommentRepository.SaveChangeAsync();
                return DeleteForeverCommentResult.Success;
            }
            else
            {
                return DeleteForeverCommentResult.CantDeletedNow;
            }
        }

        public async Task<string> CantDeleteClassCommentForeverNowMessage(int ClassCommentId)
        {
            DateTime lastEdit = await classCommentRepository.GetLastModifiedDate(ClassCommentId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این کامنت تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<ClientSideFilterCommentViewModel> GetUserCommentsAsync(int UserId, ClientSideFilterCommentViewModel filter)
        => await classCommentRepository.GetUserComments(UserId,filter);

        public async Task<ClientSideDeleteForeverCommentResult> ClientSideDeleteCommentAsync(int commentId)
        {
            var comment=await classCommentRepository.GetByIdAsync(commentId);
            if (comment == null||comment.IsDeleted==true)
                return ClientSideDeleteForeverCommentResult.NotFound;

            await classCommentReactionRepository.DeleteCommentReactions(commentId);
            classCommentRepository.Delete(comment);
            await classCommentRepository.SaveChangeAsync();

            #region Deleting Cached ClassComments
            await CacheExtensions.InvalidateCacheKey(cacheService, CacheKeys.ClassComment + comment.SportClassId.ToString());
            #endregion
            return ClientSideDeleteForeverCommentResult.Success;
        }
    }
}
