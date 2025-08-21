using GreenHeart.Domain.ViewModels.Essays.EssayCategories;

namespace GreenHeart.Application.Services.Interfaces.EssayCategorys
{
    public interface IEssayCategoryService
    {
        Task<CreateEssayCategoryResult> CreateEssayCategoryAsync(CreateEssayCategoryViewModel model);
        Task<UpdateEssayCategoryViewModel> GetEssayCategoryForEdit(int EssayCategoryId);
        Task<UpdateEssayCategoryResult> UpdateEssayCategoryAsync(UpdateEssayCategoryViewModel model);
        Task<DeleteEssayCategoryResult> DeleteEssayCategoryAsync(int EssayCategoryId);
        Task<FilterEssayCategoryViewModel> FilterEssayCategorysAsync(FilterEssayCategoryViewModel filter, int EssayCategoryParentId);
        Task<AdminSideDetailEssayCategoryViewModel?> AdminSideDetailEssayCategoryAsync(int EssayCategoryId);
        Task<DeleteForeverEssayCategoryResult> DeleteEssayCategoryForever(int EssayCategoryId);
        Task<string> CantDeleteEssayCategoryForeverNowMessage(int EssayCategoryId);
        Task<List<EssayCategoryViewModel>> GetAllChildsEssayCategoriesAsync();
    }
}
