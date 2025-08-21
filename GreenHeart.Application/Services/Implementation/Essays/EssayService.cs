using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Essays;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using GreenHeart.Infra.Data.Repositories.Essays;
using GreenHeart.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenHeart.Domain.Interfaces.Essays;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.KeyWords;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Statics;
using GreenHeart.Domain.Models.Gyms;

namespace GreenHeart.Application.Services.Implementation.Essays
{
    public class EssayService(IEssayRepository essayRepository,
        IEssayCategoryRepository essayCategoryRepository,
        ICacheService cacheService,
        IUserRepository userRepository,
        IKeyWordRepository keyWordRepository) : IEssayService
    {
        public async Task<AdminSideDetailEssayViewModel?> AdminSideDetailEssayAsync(int EssayId)
        {
            var Essay = await essayRepository.GetByIdAsync(EssayId);
            if (Essay == null)
                return null;
            return new AdminSideDetailEssayViewModel()
            {
                Id = EssayId,
                Title = Essay.Title,
                EssayCategory = await essayCategoryRepository.GetEssayCategoryViewModel(Essay.EssayCagtegoryId),
                Content = Essay.Content,
                Excerpt = Essay.Excerpt,
                keyWords = await keyWordRepository.GetEssayKeyWordsAsync(EssayId),
                CreatedDate = Essay.CreatedDate,
                LastModifiedDate = Essay.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(Essay.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(Essay.LastModifiedBy),
                CreatedById = Essay.CreatedBy,
                LastModifiedById = Essay.LastModifiedBy,
                IsDeleted = Essay.IsDeleted
            };
        }

        public async Task<string> CantDeleteEssayForeverNowMessage(int EssayId)
        {
            DateTime lastEdit = await essayRepository.GetLastModifiedDate(EssayId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این مقاله تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateEssayResult> CreateEssayAsync(CreateEssayViewModel model)
        {
            if(essayCategoryRepository.IsCategoryHasChild(model.EssayCagtegoryId)) 
                return CreateEssayResult.CantSelectParentCategory;
            if (await essayRepository.ExistTitleForEssayAsync(model.Title))
                return CreateEssayResult.TitleDuplicated;

            Essay Essay = new()
            {
                Title = model.Title,
                Content = model.Content,
                EssayCagtegoryId = model.EssayCagtegoryId,
                Excerpt = model.Excerpt
            };
            string slug = model.Title.GenerateSlug();
            if (await essayRepository.ExistSpecificSlug(slug))
            {
                slug = await essayRepository.PutSpecificSlug(slug);
            }
            Essay.Slug = slug;
            #region Image
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SavingPath.EssayPath);
                Essay.ImageUrl = imageName;
            }
            #endregion
            await essayRepository.InserAsync(Essay);
            await essayRepository.SaveChangeAsync();
            #region Deleting Cached Essays
            //TODO

            #endregion
            return CreateEssayResult.Success;
        }

        public async Task<DeleteEssayResult> DeleteEssayAsync(int EssayId)
        {
            var Essay = await essayRepository.GetByIdAsync(EssayId);
            if (Essay == null)
                return DeleteEssayResult.EssayNotFound;
            if (Essay.IsDeleted == true)
                return DeleteEssayResult.EssayAlreadyDeleted;
            #region Deleting Essay Image
            if (Essay.ImageUrl != null)
            {
                Essay.ImageUrl.DeleteImage(SavingPath.EssayPath);
                Essay.ImageUrl = null;
            }
            #endregion
            Essay.IsDeleted = true;
            essayRepository.Update(Essay);
            await essayRepository.SaveChangeAsync();
            #region Deleting Cached Essay
            //TODO
            #endregion
            return DeleteEssayResult.Success;
        }

        public async Task<DeleteForeverEssayResult> DeleteEssayForever(int EssayId)
        {
            var Essay = await essayRepository.GetByIdAsync(EssayId);

            if (Essay == null)
                return DeleteForeverEssayResult.NotFound;

            if (Essay.IsDeleted == false)
                return DeleteForeverEssayResult.FirstDeleteSimple;
            DateTime lastDate = await essayRepository.GetLastModifiedDate(EssayId);
            if (lastDate.SixMonthPassed())
            {
                essayRepository.Delete(Essay);
                await essayRepository.SaveChangeAsync();
                return DeleteForeverEssayResult.Success;
            }
            else
            {
                return DeleteForeverEssayResult.CantDeletedNow;
            }
        }

        public async Task<ClientSideFilterEssayViewModel> FilterClientSideEssayAsync(ClientSideFilterEssayViewModel Filter)
        => await essayRepository.ClientSideFilterEssay(Filter);

        public async Task<FilterEssayViewModel> FilterEssaysAsync(FilterEssayViewModel filter)
        => await essayRepository.FilterEssay(filter);

        public async Task<ClientSideEssayDetaillViewModel> GetClientSideEssayBySlugAsync(string slug)
        {
            var essay=await essayRepository.GetEssayBySlugAsync(slug);
            if (essay == null)
                return null;
            return new ClientSideEssayDetaillViewModel
            {
                Slug = essay.Slug,
                Content = essay.Content,
                CreatedDate = essay.CreatedDate,
                EssayCagtegoryId = essay.EssayCagtegoryId,
                EssayCategory = essay.essayCategory.Title,
                EssayCategorySlug=essay.essayCategory.Slug,
                ImageUrl = essay.ImageUrl,
                Title = essay.Title,
                KeyWords = await keyWordRepository.GetEssayKeyWordsAsync(essay.Id)
            };
        }

        public async Task<UpdateEssayViewModel> GetEssayForEdit(int EssayId)
        {
            var Essay = await essayRepository.GetByIdAsync(EssayId);
            if (Essay == null)
                return null;
            return new UpdateEssayViewModel()
            {
                Id = Essay.Id,
                Content = Essay.Content,
                EssayCagtegoryId = Essay.EssayCagtegoryId,
                Excerpt = Essay.Excerpt,
                IsDeleted = Essay.IsDeleted,
                Title = Essay.Title,
                ImageUrl = Essay.ImageUrl
            };
        }

        public async Task<UpdateEssayResult> UpdateEssayAsync(UpdateEssayViewModel model)
        {
            var Essay = await essayRepository.GetByIdAsync(model.Id);
            if (Essay == null)
                return UpdateEssayResult.EssayNotFound;
            if (essayCategoryRepository.IsCategoryHasChild(model.EssayCagtegoryId))
                return UpdateEssayResult.CantSelectParentCategory;
            if (await essayRepository.ExistTitleForEssayAsync(model.Title, model.Id))
                return UpdateEssayResult.TitleDuplicated;

            #region Update Essay
            if (Essay.Title != model.Title)
            {
                Essay.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                if (await essayRepository.ExistSpecificSlug(slug))
                {
                    slug = await essayRepository.PutSpecificSlug(slug);
                }
                Essay.Slug = slug;
            }
            Essay.Content = model.Content;
            Essay.Excerpt = model.Excerpt;
            Essay.EssayCagtegoryId = model.EssayCagtegoryId;
            #region Update Image
            if (model.NewImage != null)
            {
                if (Essay.ImageUrl != null)
                {
                    Essay.ImageUrl.DeleteImage(SavingPath.EssayPath);
                    Essay.ImageUrl = null;
                }
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, SavingPath.EssayPath);
                Essay.ImageUrl = imageName;
            }
            #endregion
            essayRepository.Update(Essay);
            await essayRepository.SaveChangeAsync();
            #endregion
            #region Deleting Cached Essay Categories
            //TODO
            #endregion
            return UpdateEssayResult.Success;
        }
    }
}
