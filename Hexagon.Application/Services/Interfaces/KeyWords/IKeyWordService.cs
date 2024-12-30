using Hexagon.Domain.ViewModels.KeyWords;
using Hexagon.Domain.ViewModels.Users.Roles;

namespace Hexagon.Application.Services.Interfaces.KeyWords
{
    public interface IKeyWordService
    {
        Task<CreatekeyWordResult> CreateKeyWordAsync(CreatekeyWordViewModel model);
        Task<UpdatekeyWordViewModel> GetKeyWordForEdit(int KeyWordId);
        Task<UpdatekeyWordResult> UpdateKeyWordAsync(UpdatekeyWordViewModel model);
        Task<DeletekeyWordResult> DeleteKeyWordAsync(int KeyWordId);
        Task<FilterkeyWordViewModel> FilterKeyWordsAsync(FilterkeyWordViewModel filter, int sportClassId);
        Task<AdminSideDetailKeyWordViewModel?> AdminSideDetailKeyWordAsync(int KeyWordId);
        Task<DeleteForeverkeyWordResult> DeleteKeyWordForever(int KeyWordId);
        Task<string> CantDeleteKeyWordForeverNowMessage(int KeyWordId);
    }
}
