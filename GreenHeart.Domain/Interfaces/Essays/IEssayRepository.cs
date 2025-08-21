using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Essays
{
    public interface IEssayRepository:IGenericRepository<Essay>
    {
        Task<bool> ExistTitleForEssayAsync(string title);
        Task<bool> ExistTitleForEssayAsync(string title, int essayId);
        Task<ICollection<EssayViewModel>?> GetEssaysForSpecificCategory(int essayCategoryId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<bool> ExistSpecificSlug(string slug);
        Task<string> PutSpecificSlug(string slug);
        Task<FilterEssayViewModel> FilterEssay(FilterEssayViewModel filter);
        Task<bool> ExistEssayForSpecificCategory(int essayCategoryId);
        Task<ClientSideFilterEssayViewModel> ClientSideFilterEssay(ClientSideFilterEssayViewModel filter);
        Task<Essay?> GetEssayBySlugAsync(string slug);
    }
}
