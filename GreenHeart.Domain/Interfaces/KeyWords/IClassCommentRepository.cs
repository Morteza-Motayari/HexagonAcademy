using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.ViewModels.KeyWords;

namespace GreenHeart.Domain.Interfaces.KeyWords
{
    public interface IKeyWordRepository: IGenericRepository<KeyWord>
    {
        Task<bool> ExistKeyForClassAsync(string keyword,int classId);
        Task<bool> ExistKeyForClassAsync(string keyword, int keyId,int classId);
        Task<FilterkeyWordViewModel> FilterKeyWordAsync(FilterkeyWordViewModel filter, int sportClassId);
        Task<KeyWord?> GetKeyWordWithDetail(int keyId);
        Task<List<KeyWord>> GetClassKeyWordsAsync(int classId);
        Task<List<KeyWord>> GetEssayKeyWordsAsync(int essayId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
