using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces.Essays;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.Domain.ViewModels.KeyWords;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Essays
{
    public class EssayCategoryRepository : GenericRepository<EssayCategory>, IEssayCategoryRepository
    {
        private readonly GreenHeartContext _db;

        public EssayCategoryRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistSpecificSlug(string slug)
        => await _db.EssayCategories.AnyAsync(u => u.Slug == slug && u.IsDeleted == false);

        public async Task<bool> ExistTitleForEssayCategoryAsync(string title)
        => await _db.EssayCategories.AnyAsync(s => s.Title == title && !s.IsDeleted);

        public async Task<bool> ExistTitleForEssayCategoryAsync(string title, int essayCategoryId)
        => await _db.EssayCategories.AnyAsync(s => s.Title == title && !s.IsDeleted && s.Id != essayCategoryId);

        public async Task<FilterEssayCategoryViewModel> FilterEssayCategory(FilterEssayCategoryViewModel filter, int? EssayCategoryParentId)
        {
            var query = _db.EssayCategories.Include(ec => ec.CategoryParent).AsQueryable();
            if (EssayCategoryParentId.HasValue)
                query.Where(ec => ec.EssayCategoryParentId == EssayCategoryParentId);

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

            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }
            if (filter.ParentTitle != null)
            {
                query = query.Where(r => r.CategoryParent.Title.Contains(filter.ParentTitle));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new EssayCategoryViewModel
            {
                Id = u.Id,
                Title = u.Title,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted,
                EssayCategoryParentId = u.EssayCategoryParentId,
                EssayCategoryParent = u.CategoryParent.Title,
                IsHaveCategoryChild = _db.EssayCategories.Where(ec => ec.EssayCategoryParentId == u.Id && ec.IsDeleted == false).Any()
            }));
            return filter;
        }

        public async Task<List<EssayCategoryViewModel>> GetAllChildsEssayCategories()
          => await _db.EssayCategories.Include(e => e.CategoryChilds).Where(ec => ec.CategoryChilds.Where(ch => ch.IsDeleted == false).Count() == 0)
            .Select(essayCateory => new EssayCategoryViewModel
            {
                Id = essayCateory.Id,
                Title = essayCateory.Title
            }).ToListAsync();

        public async Task<List<EssayCategoryViewModel>> GetChildsForSpeceficEssayCategories(int essayCategoryId)
        {
            List<EssayCategoryViewModel> essayCategoryChildViewModels = new List<EssayCategoryViewModel>();
            var essayCategories = await _db.EssayCategories.Include(e => e.CategoryChilds).Where(ec => ec.EssayCategoryParentId == essayCategoryId && ec.IsDeleted == false).ToListAsync();
            bool IsStillExistCategoryParent = essayCategories.Any();
            int i = 0;
            while (IsStillExistCategoryParent)
            {
                if (essayCategories[i].CategoryChilds.Count(ec => ec.IsDeleted == false) == 0)
                {
                    essayCategoryChildViewModels.Add(new EssayCategoryViewModel
                    {
                        Id = essayCategories[i].Id,
                        Title = essayCategories[i].Title,
                        EssayCategoryParentId = essayCategories[i].EssayCategoryParentId,
                        CreatedDate = essayCategories[i].CreatedDate
                    });
                }
                else
                {
                    GetSubChildsForSpeceficEssayCategory(essayCategories[i], essayCategories, essayCategoryChildViewModels);
                }
                i++;
                if (!essayCategories.Contains(essayCategories[i]))
                {
                    IsStillExistCategoryParent=false;
                }
            }
            return essayCategoryChildViewModels;
        }

        private void GetSubChildsForSpeceficEssayCategory(EssayCategory essayCategory,List<EssayCategory> essayCategories, List<EssayCategoryViewModel> essayCategoryChildViewModels)
        {
            var essayCategoriesForThisChild =_db.EssayCategories.Include(e => e.CategoryChilds).Where(ec => ec.EssayCategoryParentId == essayCategory.Id && ec.IsDeleted == false).ToList();
            foreach (var category in essayCategoriesForThisChild)
            {
                if (category.CategoryChilds.Count(ec => ec.IsDeleted == false) == 0)
                {
                    essayCategoryChildViewModels.Add(new EssayCategoryViewModel
                    {
                        Id = category.Id,
                        Title = category.Title,
                        EssayCategoryParentId = category.EssayCategoryParentId,
                        CreatedDate = category.CreatedDate
                    });
                }
                else
                {
                    essayCategories.Add(category);
                }
            }
        }
        public async Task<EssayCategoryViewModel?> GetEssayCategoryViewModel(int EssayCategoryid)
        => await _db.EssayCategories.Include(ec => ec.CategoryParent).Where(ec => ec.Id == EssayCategoryid).Select(e => new EssayCategoryViewModel
        {
            Id = e.Id,
            EssayCategoryParent = e.CategoryParent.Title,
            EssayCategoryParentId = e.CategoryParent.Id,
            CreatedDate = e.CreatedDate,
            Title = e.Title,
            IsDeleted = e.IsDeleted
        }).FirstOrDefaultAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.EssayCategories.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public bool IsCategoryHasChild(int categoryId)
        => _db.EssayCategories.Where(ec => ec.EssayCategoryParentId == categoryId && ec.IsDeleted == false).Any();

        public async Task<string> PutSpecificSlug(string slug)
        {
            slug = slug + $"-{1}";
            if (await ExistSpecificSlug(slug))
            {
                int lastIndex = slug.LastIndexOf('-');
                int Count = int.Parse(slug.Substring(lastIndex + 1));
                while (await ExistSpecificSlug(slug))
                {
                    Count++;
                    slug = slug.Substring(0, lastIndex + 1) + $"{Count}";
                }
            }
            return slug;
        }

        public async Task<EssayCategory?> GetEssayCategoryBySlugAsync(string slug)
        => await _db.EssayCategories.Where(s=>s.Slug == slug&&s.IsDeleted==false).FirstOrDefaultAsync();
    }
}
