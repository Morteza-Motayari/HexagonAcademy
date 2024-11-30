using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Hexagon.Infra.Data.DataExtensions;
using Hexagon.Domain.Models.Records;
using System.Collections.ObjectModel;

namespace Hexagon.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly HexagonContext _db;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRepository(HexagonContext db, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository) : base(db)
        {
            _db = db;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> ExistMobileAsync(string mobile)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.IsDeleted == false);

        public async Task<bool> ExistMobileAsync(string mobile, int id)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id != id && u.IsDeleted == false);


        public async Task<FilterUserViewModel> FilteUsersAsync(FilterUserViewModel filter)
        {
            var query = _db.Users.AsQueryable();

            #region Filter Search

            if (filter.Name != null)
            {
                query = query.Where(r => r.FirstName.Contains(filter.Name) || r.LastName.Contains(filter.Name)).Distinct();
            }
            if (filter.PhoneNumber != null)
            {
                query = query.Where(r => r.PhoneNumber.Contains(filter.PhoneNumber));
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
                        query = query.Where(u => u.Gender == UserGender.Male);
                        break;
                    case FilterUserGender.Female:
                        query = query.Where(u => u.Gender == UserGender.Female);
                        break;
                }
            }

            if (filter.Situation != null)
            {
                switch (filter.Situation)
                {
                    case FilterUserSituation.All:
                        break;
                    case FilterUserSituation.Cadre:
                        query = query.Where(u => u.Situation == UserSituation.Cadre);
                        break;
                    case FilterUserSituation.Athlete:
                        query = query.Where(u => u.Situation == UserSituation.Athlete);
                        break;
                    case FilterUserSituation.Trainer:
                        query = query.Where(u => u.Situation == UserSituation.Trainer);
                        break;
                }
            }

            if (filter.userStatus != null)
            {
                switch (filter.userStatus)
                {
                    case FilterUserStatus.All:
                        break;
                    case FilterUserStatus.Active:
                        query = query.Where(u => u.Status == UserStatus.Active);
                        break;
                    case FilterUserStatus.NotActive:
                        query = query.Where(u => u.Status == UserStatus.NotActive);
                        break;
                    case FilterUserStatus.Ban:
                        query = query.Where(u => u.Status == UserStatus.Ban);
                        break;
                }
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                city = u.city,
                PhoneNumber = u.PhoneNumber,
                NationalCode = u.NationalCode,
                BirthDay = u.BirthDay,
                email = u.email,
                Gender = u.Gender,
                Situation = u.Situation,
                Status = u.Status,
                Avatar = u.Avatar,
                CreatedDate = u.CreatedDate,
                IsDeleted = u.IsDeleted
            }));
            return filter;
        }

        public async Task<ReadOnlyCollection<UserViewModel>?> GetAllUsersForOptionsAsync(string term)
        {
            var data = await _db.Users.Where(u => u.Status == UserStatus.Active && u.IsDeleted == false).Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                BirthDay = u.BirthDay,
                email = u.email,
                Gender = u.Gender,
                Avatar = u.Avatar
            }).ToListAsync();

            var search = data.Where(a => a.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase)
            || a.LastName.Contains(term, StringComparison.OrdinalIgnoreCase)
            || a.PhoneNumber.Contains(term, StringComparison.OrdinalIgnoreCase)
            || a.email.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
            return search;
        }


        public async Task<User?> GetbyMobileAndPassword(string mobile, string password)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.Password == password && u.IsDeleted == false);

        public async Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.VerificationCode == verificationCode && u.IsDeleted == false);

        public async Task<User?> GetByMobileAsync(string mobile)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.IsDeleted == false);

        public async Task<string?> GetJustUserName(int? userId)
        {
            if (userId == null)
                return null;
            var user = await _db.Users.Where(u => u.Id == userId).Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).FirstAsync();
            return user?.FirstName + " " + user?.LastName;
        }

        public async Task<ClientSideUpdateUserViewModel?> GetUserForUpdateClientSide(int userId)
        => await _db.Users.Select(u => new ClientSideUpdateUserViewModel
        {
            Id = userId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            //BirthDay = u.BirthDay.ToShamsi(),
            Avatar = u.Avatar,
            Gender = u.Gender,
            email = u.email,
            city = u.city,
            NationalCode = u.NationalCode
        }).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<UserClientSideView?> GetUserForViewClientSide(int userId)
        => await _db.Users.Where(u => u.Id == userId).Select(u => new UserClientSideView
        {
            Id = userId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            BirthDay = u.BirthDay,
            Avatar = u.Avatar,
            Gender = u.Gender,
            email = u.email,
            city = u.city,
            NationalCode = u.NationalCode,
            Password = u.Password,
            PhoneNumber = u.PhoneNumber,
            Situation = u.Situation,
            Status = u.Status
        }).FirstAsync(u => u.Id == userId);

        public async Task<User?> GetUserWithChilds(int userId)
        => await _db.Users.Include(u => u.staffes).FirstOrDefaultAsync(u=>u.Id==userId);

        public async Task<bool> MobileDuplicatedAsync(string mobile, int userId)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id != userId && u.IsDeleted == false);

    }
}
