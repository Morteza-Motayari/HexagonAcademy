using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Caching;
using GreenHeart.Application.Services.Interfaces.EssayCategorys;
using GreenHeart.Application.Statics.Caches_Constatnt;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Essays;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.KeyWords;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.Domain.ViewModels.KeyWords;
using GreenHeart.Infra.Data.Migrations;
using GreenHeart.Infra.Data.Repositories.Gyms;
using GreenHeart.Infra.Data.Repositories.KeyWords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Essays
{
    public class EssayCategoryService(IEssayCategoryRepository essayCategoryRepository,
        IEssayRepository essayRepository,
        IUserRepository userRepository,
        ICacheService cacheService) : IEssayCategoryService
    {
        public async Task<AdminSideDetailEssayCategoryViewModel?> AdminSideDetailEssayCategoryAsync(int EssayCategoryId)
        {
            var essayCategory=await essayCategoryRepository.GetByIdAsync(EssayCategoryId);
            if (essayCategory == null) 
                return null;
            return new AdminSideDetailEssayCategoryViewModel()
            {
                Id = EssayCategoryId,
                Title=essayCategory.Title,
                Essays=await essayRepository.GetEssaysForSpecificCategory(EssayCategoryId),
                CreatedDate = essayCategory.CreatedDate,
                LastModifiedDate = essayCategory.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(essayCategory.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(essayCategory.LastModifiedBy),
                CreatedById = essayCategory.CreatedBy,
                LastModifiedById = essayCategory.LastModifiedBy,
                IsDeleted = essayCategory.IsDeleted
            };
        }

        public async Task<string> CantDeleteEssayCategoryForeverNowMessage(int EssayCategoryId)
        {
            DateTime lastEdit = await essayCategoryRepository.GetLastModifiedDate(EssayCategoryId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این گروه مقاله تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateEssayCategoryResult> CreateEssayCategoryAsync(CreateEssayCategoryViewModel model)
        {
            if (await essayCategoryRepository.ExistTitleForEssayCategoryAsync(model.Title))
                return CreateEssayCategoryResult.TitleDuplicated;
            if (model.EssayCategoryParentId.HasValue)
            {
                if (await essayRepository.ExistEssayForSpecificCategory((int)model.EssayCategoryParentId))
                    return CreateEssayCategoryResult.EssayCategoryHasEssaysAndCantHaveChildCategory;
            }
            
            EssayCategory essayCategory = new()
            {
                Title = model.Title,
                EssayCategoryParentId=model?.EssayCategoryParentId
            };
            string slug = model.Title.GenerateSlug();
            if (await essayCategoryRepository.ExistSpecificSlug(slug))
            {
                slug = await essayCategoryRepository.PutSpecificSlug(slug);
            }
            essayCategory.Slug = slug;

            await essayCategoryRepository.InserAsync(essayCategory);
            await essayCategoryRepository.SaveChangeAsync();
            #region Deleting Cached Category Essays
            //TODO

            #endregion
            return CreateEssayCategoryResult.Success;
        }

        public async Task<DeleteEssayCategoryResult> DeleteEssayCategoryAsync(int EssayCategoryId)
        {
            var EssayCategory = await essayCategoryRepository.GetByIdAsync(EssayCategoryId);
            if (EssayCategory == null)
                return DeleteEssayCategoryResult.EssayCategoryNotFound;
            if (EssayCategory.IsDeleted == true)
                return DeleteEssayCategoryResult.EssayCategoryAlreadyDeleted;

            EssayCategory.IsDeleted = true;
            essayCategoryRepository.Update(EssayCategory);
            await essayCategoryRepository.SaveChangeAsync();
            #region Deleting Cached EssayCategory
            //TODO
            #endregion
            return DeleteEssayCategoryResult.Success;
        }

        public async Task<DeleteForeverEssayCategoryResult> DeleteEssayCategoryForever(int EssayCategoryId)
        {
            var EssayCategory = await essayCategoryRepository.GetByIdAsync(EssayCategoryId);
            if (EssayCategory == null)
                return DeleteForeverEssayCategoryResult.NotFound;
            if (EssayCategory.IsDeleted == false)
                return DeleteForeverEssayCategoryResult.FirstDeleteSimple;
            DateTime lastDate = await essayCategoryRepository.GetLastModifiedDate(EssayCategoryId);
            if (lastDate.SixMonthPassed())
            {
                essayCategoryRepository.Delete(EssayCategory);
                await essayCategoryRepository.SaveChangeAsync();
                return DeleteForeverEssayCategoryResult.Success;
            }
            else
            {
                return DeleteForeverEssayCategoryResult.CantDeletedNow;
            }
        }

        public async Task<FilterEssayCategoryViewModel> FilterEssayCategorysAsync(FilterEssayCategoryViewModel filter, int EssayCategoryParentId)
        => await essayCategoryRepository.FilterEssayCategory(filter, EssayCategoryParentId);

        public async Task<List<EssayCategoryViewModel>> GetAllChildsEssayCategoriesAsync()
        => await essayCategoryRepository.GetAllChildsEssayCategories();

        public async Task<UpdateEssayCategoryViewModel> GetEssayCategoryForEdit(int EssayCategoryId)
        {
            var EssayCategory = await essayCategoryRepository.GetByIdAsync(EssayCategoryId);
            if (EssayCategory == null)
                return null;
            return new UpdateEssayCategoryViewModel()
            {
                Id = EssayCategory.Id,
                IsDeleted = EssayCategory.IsDeleted,
                Title = EssayCategory.Title
            };
        }

        public async Task<UpdateEssayCategoryResult> UpdateEssayCategoryAsync(UpdateEssayCategoryViewModel model)
        {
            var EssayCategory = await essayCategoryRepository.GetByIdAsync(model.Id);
            if (EssayCategory == null)
                return UpdateEssayCategoryResult.EssayCategoryNotFound;
            if (await essayCategoryRepository.ExistTitleForEssayCategoryAsync(model.Title,model.Id))
                return UpdateEssayCategoryResult.TitleDuplicated;

            #region Update EssayCategory
            if (EssayCategory.Title != model.Title)
            {
                EssayCategory.Title = model.Title;
                string slug = model.Title.GenerateSlug();
                if (await essayCategoryRepository.ExistSpecificSlug(slug))
                {
                    slug = await essayCategoryRepository.PutSpecificSlug(slug);
                }
                EssayCategory.Slug = slug;
            }
            essayCategoryRepository.Update(EssayCategory);
            await essayCategoryRepository.SaveChangeAsync();
            #endregion
            #region Deleting Cached Essay Categories
            //TODO
            #endregion
            return UpdateEssayCategoryResult.Success;
        }
    }
}
