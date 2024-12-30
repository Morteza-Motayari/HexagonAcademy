using Hexagon.Domain.Models.KeyWords;
using Hexagon.Domain.ViewModels.KeyWords;

namespace Hexagon.Domain.Interfaces.KeyWords
{
    public interface IKeyWordRepository: IGenericRepository<KeyWord>
    {
        Task<bool> ExistKeyForClassAsync(string keyword);
        Task<bool> ExistKeyForClassAsync(string keyword, int keyId);
        Task<FilterkeyWordViewModel> FilterKeyWordAsync(FilterkeyWordViewModel filter, int sportClassId);
        Task<KeyWord?> GetKeyWordWithDetail(int keyId);
        Task<List<KeyWord>> GetClassKeyWordsAsync(int classId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
