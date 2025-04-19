using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces.Records;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Records.Experiences;
using GreenHeart.Domain.ViewModels.Records.RecordCategories;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Records
{
    public class RecordCategoryRepository:GenericRepository<RecordCategory>, IRecordCategoryRepository
    {
        private readonly GreenHeartContext _db;

        public RecordCategoryRepository(GreenHeartContext db):base(db) 
        {
            _db = db;
        }

        public async Task<bool> CategoryExistForStaffAsync(int staffId, string tilte)
        => await _db.RecordCategories.Where(r => !r.IsDeleted).AnyAsync(r => r.StaffId == staffId && r.Title == tilte);

        public async Task<bool> CategoryExistForStaffAsync(int id, int staffId, string tilte)
        => await _db.RecordCategories.Where(r => !r.IsDeleted).AnyAsync(r => r.Id!=id&& r.StaffId == staffId && r.Title == tilte);

        public async Task<FilterRecordCategoryViewModel> FilterRecordCategoryAsync(int staffId,FilterRecordCategoryViewModel filter)
        {
            var query = _db.RecordCategories.Where(r=>r.StaffId==staffId).Include(u => u.Trainer).AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case ExistingStatus.All:
                    break;
                case ExistingStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case ExistingStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }

            if (filter.Title != null)
            {
                query = query.Where(r => r.Title.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new RecordCategoryViewModel
            {
                Id = u.Id,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate,
                Title = u.Title,
                StaffId = u.StaffId
            }));
            return filter;
        }

        public async Task<string> GetCategoryNameAsync(int id)
        => await _db.RecordCategories.Where(c => c.Id == id).Select(r => r.Title).FirstOrDefaultAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.RecordCategories.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<bool> IsCategoryDeletedAsync(int id)
        => await _db.RecordCategories.Where(c=>c.Id==id)
            .Select(t=>t.IsDeleted).FirstOrDefaultAsync();

        public async Task<List<RecordCategoryViewModel>?> ListRecordCategoryForStaff(int staffId)
        => await _db.RecordCategories.Where(r=>r.StaffId==staffId)
            .Select(s => new RecordCategoryViewModel
            {
                Id = s.Id,
                Title= s.Title,
                CreatedDate= s.CreatedDate,
                IsDeleted= s.IsDeleted
            }).ToListAsync();
    }
}
