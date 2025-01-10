using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Interfaces.KeyWords;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.KeyWords;
using Hexagon.Domain.ViewModels.Gyms.Sports;
using Hexagon.Domain.ViewModels.KeyWords;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.KeyWords
{
    public class KeyWordRepository : GenericRepository<KeyWord>, IKeyWordRepository
    {
        private readonly HexagonContext _db;

        public KeyWordRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistKeyForClassAsync(string keyword, int classId)
        => await _db.KeyWords.AnyAsync(s => s.Key == keyword&&!s.IsDeleted&&s.ClassId==classId);

        public async Task<bool> ExistKeyForClassAsync(string keyword, int keyId, int classId)
        => await _db.KeyWords.AnyAsync(s => s.Key == keyword && !s.IsDeleted&&s.Id!=keyId&&s.ClassId==classId);

        public async Task<FilterkeyWordViewModel> FilterKeyWordAsync(FilterkeyWordViewModel filter, int sportClassId)
        {
            var query = _db.KeyWords.Where(k=>k.ClassId==sportClassId).AsQueryable();

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

            if (filter.Key != null)
            {
                query = query.Where(r => r.Key.Contains(filter.Key));
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new keyWordViewModel
            {
                Id = u.Id,
                Key = u.Key,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted
            }));
            return filter;
        }

        public async Task<List<KeyWord>> GetClassKeyWordsAsync(int classId)
        => await _db.KeyWords.Where(c=>c.ClassId==classId&&!c.IsDeleted).ToListAsync();

        public async Task<KeyWord?> GetKeyWordWithDetail(int keyId)
        => await _db.KeyWords.Include(a => a.sportClass).FirstOrDefaultAsync(u => u.Id == keyId);

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.KeyWords.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
