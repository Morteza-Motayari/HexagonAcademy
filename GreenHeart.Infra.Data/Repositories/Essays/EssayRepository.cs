using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces.Essays;
using GreenHeart.Domain.Interfaces.KeyWords;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenHeart.Infra.Data.Repositories.Essays
{
    public class EssayRepository : GenericRepository<Essay>, IEssayRepository
    {
        private readonly GreenHeartContext _db;
        private readonly IEssayCategoryRepository _essayCategoryRepository;

        public EssayRepository(GreenHeartContext db,IEssayCategoryRepository essayCategoryRepository) : base(db)
        {
            _db = db;
            _essayCategoryRepository = essayCategoryRepository;
        }

        public async Task<ClientSideFilterEssayViewModel> ClientSideFilterEssay(ClientSideFilterEssayViewModel filter)
        {
            var query = _db.Essays.Include(u => u.essayCategory).Where(s=>s.IsDeleted==false).AsQueryable();

            #region Filter Search
            

            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }
            if (filter.EssayCategoryId.HasValue)
            {

                var essayCategoriesChilds = await _essayCategoryRepository.GetChildsForSpeceficEssayCategories(filter.EssayCategoryId.Value);
                List<int> essayCategoriesChildsId = essayCategoriesChilds.Select(c => c.Id).ToList();
                query = query.Where(ec => essayCategoriesChildsId.Contains(ec.EssayCagtegoryId));
            }
            if(!string.IsNullOrEmpty(filter.EssayCategorySlug))
            {
                var essayCategory=await _essayCategoryRepository.GetEssayCategoryBySlugAsync(filter.EssayCategorySlug);
                query = query.Where(ec =>ec.EssayCagtegoryId==essayCategory.Id);
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ClientSideEssayViewModel
            {
                Title = u.Title,
                EssayCagtegoryId = u.EssayCagtegoryId,
                Excerpt = u.Excerpt,
                EssayCategory = u.essayCategory.Title,
                CreatedDate = u.CreatedDate,
                ImageUrl = u.ImageUrl,
                Slug=u.Slug
            }));
            return filter;
        }

        public async Task<bool> ExistEssayForSpecificCategory(int essayCategoryId)
        => await _db.Essays.Where(e=>e.EssayCagtegoryId==essayCategoryId&& e.IsDeleted==false).AnyAsync();

        public async Task<bool> ExistSpecificSlug(string slug)
        => await _db.Essays.AnyAsync(u => u.Slug == slug && u.IsDeleted==false);

        public async Task<bool> ExistTitleForEssayAsync(string title)
        => await _db.Essays.AnyAsync(s => s.Title == title && !s.IsDeleted);

        public async Task<bool> ExistTitleForEssayAsync(string title, int essayId)
        => await _db.Essays.AnyAsync(s => s.Title == title && !s.IsDeleted && s.Id!=essayId);

        public async Task<FilterEssayViewModel> FilterEssay(FilterEssayViewModel filter)
        {
            var query = _db.Essays.Include(u=>u.essayCategory).AsQueryable();

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
            if(filter.EssayCategoryId.HasValue)
            {
                
                var essayCategoriesChilds=await _essayCategoryRepository.GetChildsForSpeceficEssayCategories(filter.EssayCategoryId.Value);
                List<int> essayCategoriesChildsId=essayCategoriesChilds.Select(c=> c.Id).ToList();
                query =query.Where(ec=> essayCategoriesChildsId.Contains(ec.EssayCagtegoryId));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new EssayViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Content=u.Content,
                EssayCagtegoryId = u.EssayCagtegoryId,
                Excerpt = u.Excerpt,
                EssayCategory=u.essayCategory.Title,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted,
            }));
            return filter;
        }

        public async Task<Essay?> GetEssayBySlugAsync(string slug)
        => await _db.Essays.Include(s=>s.essayCategory).Where(s=>s.Slug == slug&&s.IsDeleted!=true).FirstOrDefaultAsync();

        public async Task<ICollection<EssayViewModel>?> GetEssaysForSpecificCategory(int essayCategoryId)
        => await _db.Essays.Include(ec=>ec.essayCategory).Where(e=>e.EssayCagtegoryId==essayCategoryId)
            .Select(e=>new EssayViewModel
            {
                Title = e.Title,
                Content = e.Content,
                EssayCagtegoryId = essayCategoryId,
                Excerpt = e.Excerpt,
                Id = e.Id,
                EssayCategory=e.essayCategory.Title
            }).ToListAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Essays.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

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
    }
}
