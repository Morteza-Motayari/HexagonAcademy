using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.ViewModels.Essays.EssayCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Essays
{
    public interface IEssayCategoryRepository : IGenericRepository<EssayCategory>
    {
        Task<bool> ExistTitleForEssayCategoryAsync(string title);
        Task<bool> ExistTitleForEssayCategoryAsync(string title, int essayCategoryId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<bool> ExistSpecificSlug(string slug);
        Task<string> PutSpecificSlug(string slug);
        Task<FilterEssayCategoryViewModel> FilterEssayCategory(FilterEssayCategoryViewModel filter, int? EssayCategoryParentId);
        Task<EssayCategoryViewModel?> GetEssayCategoryViewModel(int EssayCategoryid);
        bool IsCategoryHasChild(int categoryId);
        Task<List<EssayCategoryViewModel>> GetAllChildsEssayCategories();
        Task<List<EssayCategoryViewModel>> GetChildsForSpeceficEssayCategories(int essayCategoryId);
        Task<EssayCategory?> GetEssayCategoryBySlugAsync(string slug);
    }
}
