using GreenHeart.Domain.ViewModels.Records.RecordCategories;

namespace GreenHeart.Application.Services.Interfaces.Records
{
    public interface IRecordCategoryService
    {
        Task<CreateRecordCategoryResult> CreateRecordCategoryAsync(CreateRecordCategoryViewModel model);
        Task<UpdateRecordCategoryViewModel> GetRecordCategoryForEdit(int recordCategoryId);
        Task<UpdateRecordCategoryResult> UpdateRecordCategoryAsync(UpdateRecordCategoryViewModel model);
        Task<DeleteRecordCategoryResult> DeleteRecordCategoryAsync(int recordCategoryId);
        Task<List<RecordCategoryViewModel>?> ListRecordCategoryesAsync(int staffId);
        Task<FilterRecordCategoryViewModel> FilterRecordCategoryesAsync(int staffId,FilterRecordCategoryViewModel filter);
        Task<AdminSideDetailRecordCategoryViewModel?> AdminSideDetailRecordCategoryAsync(int recordCategoryId);
        Task<DeleteForeverRecordCategoryResult> DeleteRecordCategoryForever(int recordCategoryId);
        Task<string> CantDeleteRecordCategoryForeverNowMessage(int recordCategoryId);
        Task<bool> IsRecordCategoryDeletedAsync(int categoryId);
    }
}
