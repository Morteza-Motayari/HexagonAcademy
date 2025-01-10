using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.KeyWords;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.KeyWords;
using Hexagon.Domain.Models.KeyWords;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.KeyWords;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Infra.Data.Repositories;
using Hexagon.Infra.Data.Repositories.Users;

namespace Hexagon.Application.Services.Implementation.KeyWords
{
    public class KeyWordService(IKeyWordRepository keyWordRepository
        ,IUserRepository userRepository):IKeyWordService
    {
        public async Task<AdminSideDetailKeyWordViewModel?> AdminSideDetailKeyWordAsync(int KeyWordId)
        {
            var keyword = await keyWordRepository.GetKeyWordWithDetail(KeyWordId);
            if (keyword == null)
                return null;
            AdminSideDetailKeyWordViewModel? Detail = new()
            {
                Id = KeyWordId,
                Key=keyword.Key,
                SportClassId=keyword.ClassId,
                SportClass=keyword?.sportClass?.Title,
                CreatedDate = keyword.CreatedDate,
                LastModifiedDate = keyword.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(keyword.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(keyword.LastModifiedBy),
                CreatedById = keyword.CreatedBy,
                LastModifiedById = keyword.LastModifiedBy,
                IsDeleted = keyword.IsDeleted
            };
            return Detail;
        }

        public async Task<string> CantDeleteKeyWordForeverNowMessage(int KeyWordId)
        {
            DateTime lastEdit = await keyWordRepository.GetLastModifiedDate(KeyWordId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این کلمه تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreatekeyWordResult> CreateKeyWordAsync(CreatekeyWordViewModel model)
        {
            if (await keyWordRepository.ExistKeyForClassAsync(model.Key,model.SportClassId))
                return CreatekeyWordResult.KeyDuplicated;

            KeyWord keyword = new()
            {
                Key = model.Key,
                ClassId = model.SportClassId
            };

            await keyWordRepository.InserAsync(keyword);
            await keyWordRepository.SaveChangeAsync();
            return CreatekeyWordResult.Success;
        }

        public async Task<DeletekeyWordResult> DeleteKeyWordAsync(int KeyWordId)
        {
            var KeyWord = await keyWordRepository.GetByIdAsync(KeyWordId);
            if (KeyWord == null)
                return DeletekeyWordResult.KeyWordNotFound;
            if (KeyWord.IsDeleted == true)
                return DeletekeyWordResult.KeyWordAlreadyDeleted;

            KeyWord.IsDeleted = true;
            keyWordRepository.Update(KeyWord);
            await keyWordRepository.SaveChangeAsync();
            return DeletekeyWordResult.Success;
        }

        public async Task<DeleteForeverkeyWordResult> DeleteKeyWordForever(int KeyWordId)
        {
            DateTime lastDate = await keyWordRepository.GetLastModifiedDate(KeyWordId);
            if (lastDate.SixMonthPassed())
            {
                var keyword = await keyWordRepository.GetByIdAsync(KeyWordId);

                if (keyword == null)
                    return DeleteForeverkeyWordResult.NotFound;

                if (keyword.IsDeleted == false)
                    return DeleteForeverkeyWordResult.FirstDeleteSimple;

                keyWordRepository.Delete(keyword);
                await keyWordRepository.SaveChangeAsync();
                return DeleteForeverkeyWordResult.Success;
            }
            else
            {
                return DeleteForeverkeyWordResult.CantDeletedNow;
            }
        }

        public async Task<FilterkeyWordViewModel> FilterKeyWordsAsync(FilterkeyWordViewModel filter, int sportClassId)
        => await keyWordRepository.FilterKeyWordAsync(filter,sportClassId);

        public async Task<UpdatekeyWordViewModel> GetKeyWordForEdit(int KeyWordId)
        {
            var KeyWord = await keyWordRepository.GetByIdAsync(KeyWordId);
            if (KeyWord == null)
                return null;
            return new UpdatekeyWordViewModel()
            {
                Id = KeyWord.Id,
                Key = KeyWord.Key,
                SportClassId = KeyWord.ClassId,
                IsDeleted = KeyWord.IsDeleted
            };
        }

        public async Task<UpdatekeyWordResult> UpdateKeyWordAsync(UpdatekeyWordViewModel model)
        {
            var KeyWord = await keyWordRepository.GetByIdAsync(model.Id);
            if (KeyWord == null)
                return UpdatekeyWordResult.KeyWordNotFound;
            if (await keyWordRepository.ExistKeyForClassAsync(model.Key, model.Id, (int)model.SportClassId))
                return UpdatekeyWordResult.KeyDuplicated;

            #region Update KeyWord
            KeyWord.Key = model.Key;
            keyWordRepository.Update(KeyWord);
            await keyWordRepository.SaveChangeAsync();
            #endregion

            return UpdatekeyWordResult.Success;
        }
    }
}
