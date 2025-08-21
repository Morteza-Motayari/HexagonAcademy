using GreenHeart.Application.Extensions;
using GreenHeart.Application.Services.Interfaces.Records;
using GreenHeart.Application.Services.Interfaces.Users;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Records;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.ViewModels.Records.RecordCategories;

namespace GreenHeart.Application.Services.Implementation.Records
{
    public class RecordCategoryService(IRecordCategoryRepository recordCategoryRepository
        ,IUserRepository userRepository
        ,IExperienceRepository experienceRepository
        ,IStaffService staffService) : IRecordCategoryService
    {
        public async Task<AdminSideDetailRecordCategoryViewModel?> AdminSideDetailRecordCategoryAsync(int recordCategoryId)
        {
            var recordCategory = await recordCategoryRepository.GetByIdAsync(recordCategoryId);
            if (recordCategory == null)
                return null;
            AdminSideDetailRecordCategoryViewModel? Detail = new()
            {
                Id = recordCategoryId,
                StaffId = recordCategory.StaffId,
                Title = recordCategory.Title,
                StaffName = await staffService.GetStaffNameAsync(recordCategory.StaffId),
                CreatedDate = recordCategory.CreatedDate,
                LastModifiedDate = recordCategory.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(recordCategory.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(recordCategory.LastModifiedBy),
                CreatedById = recordCategory.CreatedBy,
                LastModifiedById = recordCategory.LastModifiedBy,
                IsDeleted = recordCategory.IsDeleted,
                Experiences=await experienceRepository.GetExperincesForRecordCategory(recordCategoryId)
            };
            return Detail;
        }

        public async Task<string> CantDeleteRecordCategoryForeverNowMessage(int recordCategoryId)
        {
            DateTime lastEdit = await recordCategoryRepository.GetLastModifiedDate(recordCategoryId);
            int leftdays = lastEdit.HowManyDayLeftToDelete();
            return $"شما فعلا توانایی حذف مطلق این مجموعه سوابق تا {leftdays} روز آینده را ندارید.";
        }

        public async Task<CreateRecordCategoryResult> CreateRecordCategoryAsync(CreateRecordCategoryViewModel model)
        {
            if (await recordCategoryRepository.CategoryExistForStaffAsync(model.StaffId, model.Title))
                return CreateRecordCategoryResult.CategoryExisted;
            RecordCategory recordCategory = new()
            {
                Title = model.Title,
                StaffId = model.StaffId
            };
            await recordCategoryRepository.InserAsync(recordCategory);
            await recordCategoryRepository.SaveChangeAsync();
            return CreateRecordCategoryResult.Success;
        }

        public async Task<DeleteRecordCategoryResult> DeleteRecordCategoryAsync(int recordCategoryId)
        {
            var category=await recordCategoryRepository.GetByIdAsync(recordCategoryId);
            if(category == null)
                return DeleteRecordCategoryResult.RecordCategoryNotFound;

            if(category.IsDeleted==true)
                return DeleteRecordCategoryResult.RecordCategoryAlreadyDeleted;
            category.IsDeleted = true;
            recordCategoryRepository.Update(category);
            await recordCategoryRepository.SaveChangeAsync();
            return DeleteRecordCategoryResult.Success;
        }

        public async Task<DeleteForeverRecordCategoryResult> DeleteRecordCategoryForever(int recordCategoryId)
        {
            var category = await recordCategoryRepository.GetByIdAsync(recordCategoryId);

            if (category == null)
                return DeleteForeverRecordCategoryResult.RecordCategoryNotFound;

            if (category.IsDeleted == false)
                return DeleteForeverRecordCategoryResult.FirstDeleteSimple;
            DateTime lastDate = await recordCategoryRepository.GetLastModifiedDate(recordCategoryId);
            if (lastDate.SixMonthPassed())
            {
                recordCategoryRepository.Delete(category);
                await recordCategoryRepository.SaveChangeAsync();
                return DeleteForeverRecordCategoryResult.Success;
            }
            else
            {
                return DeleteForeverRecordCategoryResult.CantDeletedNow;
            }
        }

        public async Task<FilterRecordCategoryViewModel> FilterRecordCategoryesAsync(int staffId, FilterRecordCategoryViewModel filter)
        => await recordCategoryRepository.FilterRecordCategoryAsync(staffId, filter);

        public async Task<UpdateRecordCategoryViewModel> GetRecordCategoryForEdit(int recordCategoryId)
        {
            var category = await recordCategoryRepository.GetByIdAsync(recordCategoryId);
            if (category == null)
                return null;
            return new UpdateRecordCategoryViewModel()
            {
                Id = recordCategoryId,
                Title = category.Title,
                StaffId = category.StaffId,
                IsDeleted = category.IsDeleted
            };
        }

        public async Task<bool> IsRecordCategoryDeletedAsync(int categoryId)
        => await recordCategoryRepository.IsCategoryDeletedAsync(categoryId);

        public async Task<List<RecordCategoryViewModel>?> ListRecordCategoryesAsync(int staffId)
        => await recordCategoryRepository.ListRecordCategoryForStaff(staffId);

        public async Task<UpdateRecordCategoryResult> UpdateRecordCategoryAsync(UpdateRecordCategoryViewModel model)
        {
            var category=await recordCategoryRepository.GetByIdAsync(model.Id);
            if(category == null)
                return UpdateRecordCategoryResult.RecordCategoryNotFound;
            if(await recordCategoryRepository.CategoryExistForStaffAsync(category.Id,category.StaffId,category.Title))
                return UpdateRecordCategoryResult.RecordCategoryExisted;

            #region  update category
            category.Title = model.Title;
            recordCategoryRepository.Update(category);
            await recordCategoryRepository.SaveChangeAsync();
            #endregion

            return UpdateRecordCategoryResult.Success;
        }
    }
}
