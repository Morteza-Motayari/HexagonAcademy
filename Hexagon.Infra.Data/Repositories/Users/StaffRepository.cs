using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Staffs;
using Hexagon.Domain.ViewModels.Users.Staffs.Caders;
using Hexagon.Domain.ViewModels.Users.Staffs.Trainers;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Hexagon.Infra.Data.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        private readonly HexagonContext _db;
        public StaffRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> DuplicatedStaffPositionAsync(string position, int userId)
        => await _db.Staffs.AnyAsync(s => s.Position == position && s.UserId == userId && s.IsDeleted == false);

        public async Task<bool> DuplicatedStaffPositionAsync(string position, int userId, int staffId)
        => await _db.Staffs.AnyAsync(s => s.Position == position && s.UserId == userId && s.Id != staffId && s.IsDeleted == false);

        public async Task<bool> ExistActiveCaderForUser(int userId)
        => await _db.Staffs.Include(s => s.userRoles).Where(c => c.UserId == userId)
            .AnyAsync(c => c.userRoles != null && !c.IsDeleted);

        public async Task<bool> ExistStaffForUser(int userId)
        => await _db.Staffs.AnyAsync(s => s.UserId == userId && s.IsDeleted == false);

        public async Task<FilterCaderViewModel> FilterCadersAsync(FilterCaderViewModel filter)
        {
            var query = _db.Staffs.Include(s => s.user).Where(c=>c.userRoles.Any()).AsQueryable();

            #region Filter Search

            if (filter.Name != null)
            {
                query = query.Where(r => r.user.FirstName.Contains(filter.Name) || r.user.LastName.Contains(filter.Name)).Distinct();
            }
            if (filter.Position != null)
            {
                query = query.Where(r => r.Position.Contains(filter.Position));
            }
            if (filter.RoleId.HasValue)
            {
                query = query.Include(u => u.userRoles).Where(u => u.userRoles.Where(u => u.RoleId == filter.RoleId).Any()).AsQueryable();
            }
            if (filter.PhoneNumber != null)
            {
                query = query.Where(r => r.user.PhoneNumber.Contains(filter.PhoneNumber));
            }
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
            if (filter.Gender != null)
            {
                switch (filter.Gender)
                {
                    case FilterUserGender.All:
                        break;
                    case FilterUserGender.Male:
                        query = query.Where(u => u.user.Gender == UserGender.Male);
                        break;
                    case FilterUserGender.Female:
                        query = query.Where(u => u.user.Gender == UserGender.Female);
                        break;
                }
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new CaderViewModel
            {
                Id = u.Id,
                FullName = u.user.FirstName + " " + u.user.LastName,
                PhoneNumber = u.user.PhoneNumber,
                email = u.user.email,
                Gender = u.user.Gender,
                Salary = u.Salary,
                Position = u.Position,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted,
                UserId = u.user.Id
            }));
            return filter;
        }

        public async Task<FilterTrainerViewModel> FilterTrainersAsync(FilterTrainerViewModel filter)
        {
            var query = _db.Staffs.Include(s => s.user).Where(c => c.UserCertificates.Any()).AsQueryable();

            #region Filter Search

            if (filter.Name != null)
            {
                query = query.Where(r => r.user.FirstName.Contains(filter.Name) || r.user.LastName.Contains(filter.Name)).Distinct();
            }
            if (filter.Position != null)
            {
                query = query.Where(r => r.Position.Contains(filter.Position));
            }
            if (filter.CertificateId.HasValue)
            {
                query = query.Include(u => u.UserCertificates).Where(u => u.UserCertificates.Where(u => u.CertificateId == filter.CertificateId).Any()).AsQueryable();
            }
            if (filter.PhoneNumber != null)
            {
                query = query.Where(r => r.user.PhoneNumber.Contains(filter.PhoneNumber));
            }
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
            if (filter.Gender != null)
            {
                switch (filter.Gender)
                {
                    case FilterUserGender.All:
                        break;
                    case FilterUserGender.Male:
                        query = query.Where(u => u.user.Gender == UserGender.Male);
                        break;
                    case FilterUserGender.Female:
                        query = query.Where(u => u.user.Gender == UserGender.Female);
                        break;
                }
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new TrainerViewModel
            {
                Id = u.Id,
                FullName = u.user.FirstName + " " + u.user.LastName,
                PhoneNumber = u.user.PhoneNumber,
                email = u.user.email,
                Gender = u.user.Gender,
                Salary = u.Salary,
                Position = u.Position,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted,
                UserId = u.user.Id
            }));
            return filter;
        }

        public async Task<List<CaderViewModel>> GetCadersWithRoleAsync(int roleId)
        =>await _db.Staffs.Include(u=>u.user).Include(c=>c.userRoles).Where(c=>c.userRoles.Where(r=>r.RoleId==roleId).Any())
            .Select(c=>new CaderViewModel
            {
                Id=c.Id,
                FullName=c.user.FirstName+" "+c.user.LastName,
                IsDeleted=c.IsDeleted
            }).ToListAsync();

        public async Task<string> GetStaffNameAsync(int staffId)
        {
            var staff = await _db.Staffs.Include(s => s.user).FirstOrDefaultAsync(s => s.Id == staffId);
            return staff.user.FirstName+" "+staff.user.LastName;
         }

        public async Task<Staff?> GetStaffWithUser(int staffId)
        => await _db.Staffs.Include(s => s.user).FirstOrDefaultAsync(s => s.Id == staffId);

        public async Task<List<int>> GetUserStaffIds(int userId)
        => await _db.Staffs.Where(s => s.UserId == userId && s.IsDeleted == false).Select(i => i.Id).ToListAsync();

        public ReadOnlyCollection<TrainerViewModel>? ListSuitableTrainersForClassAsync(UserGender gender, int sportCertificateId)
        {
            return _db.Staffs.Include(s => s.user).Include(s => s.UserCertificates)
             .Where(s => s.user.Gender == gender && s.UserCertificates.Where(u => u.CertificateId == sportCertificateId).Any()).Select(t => new TrainerViewModel
             {
                 Id = t.Id,
                 FullName = t.user.FirstName + " " + t.user.LastName,
                 Position = t.Position,
                 PhoneNumber = t.user.PhoneNumber
             }).ToList().AsReadOnly();
        }

        public async Task<List<TrainerViewModel>?> ListSuitableTrainersForEditClassAsync(UserGender gender, int sportCertificateId)
        => await _db.Staffs.Include(s => s.user).Include(s => s.UserCertificates)
             .Where(s => s.user.Gender == gender && s.UserCertificates.Where(u => u.CertificateId == sportCertificateId).Any()).Select(t => new TrainerViewModel
             {
                 Id = t.Id,
                 FullName = t.user.FirstName + " " + t.user.LastName,
                 Position = t.Position,
                 PhoneNumber = t.user.PhoneNumber
             }).ToListAsync();
    }
}
