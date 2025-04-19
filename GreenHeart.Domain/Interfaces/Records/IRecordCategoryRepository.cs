using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.ViewModels.Records.RecordCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Records
{
    public interface IRecordCategoryRepository:IGenericRepository<RecordCategory>
    {
        Task<DateTime> GetLastModifiedDate(int id);
        Task<bool> CategoryExistForStaffAsync(int staffId, string tilte);
        Task<bool> CategoryExistForStaffAsync(int id,int staffId, string tilte);
        Task<FilterRecordCategoryViewModel> FilterRecordCategoryAsync(int staffId,FilterRecordCategoryViewModel filter);
        Task<List<RecordCategoryViewModel>?> ListRecordCategoryForStaff(int staffId);
        Task<bool> IsCategoryDeletedAsync(int id);
        Task<string> GetCategoryNameAsync(int id);
    }
}
