using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Users;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hexagon.Infra.Data.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly HexagonContext _db;
        public UserRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistMobileAsync(string mobile)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile);

        public async Task<bool> ExistNationalCodeAsync(string NationalCode)
        => await _db.Users.AnyAsync(u => u.NationalCode == NationalCode);

        public async Task<FilterUserViewModel> FilteUsersAsync(FilterUserViewModel filter)
        {
            var query = _db.Users.AsQueryable();

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

            if (filter.Name != null)
            {
                query = query.Where(r => r.FirstName.Contains(filter.Name) || r.LastName.Contains(filter.Name)).Distinct();
            }

            if(filter.Gender != null)
            {
                switch (filter.Gender)
                {
                    case UserGender.Male:
                        query = query.Where(u => u.Gender == UserGender.Male);
                        break;
                    case UserGender.Female:
                        query = query.Where(u => u.Gender == UserGender.Female);
                        break;
                }
            }

            if (filter.Situation != null)
            {
                switch (filter.Situation)
                {
                    case UserSituation.Cadre:
                        query = query.Where(u => u.Situation == UserSituation.Cadre);
                        break;
                    case UserSituation.Athlete:
                        query = query.Where(u => u.Situation == UserSituation.Athlete);
                        break;
                    case UserSituation.Trainer:
                        query = query.Where(u => u.Situation == UserSituation.Trainer);
                        break;
                }
            }

            if (filter.userStatus != null)
            {
                switch (filter.userStatus)
                {
                    case UserStatus.Active:
                        query = query.Where(u => u.Status == UserStatus.Active);
                        break;
                    case UserStatus.NotActive:
                        query = query.Where(u => u.Status == UserStatus.NotActive);
                        break;
                    case UserStatus.Ban:
                        query = query.Where(u => u.Status == UserStatus.Ban);
                        break;
                }
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName=u.FirstName,
                LastName=u.LastName,
                city=u.city,
                PhoneNumber=u.PhoneNumber,
                NationalCode=u.NationalCode,
                BirthDay=u.BirthDay,
                email=u.email,
                Gender=u.Gender,
                Situation=u.Situation,
                Status=u.Status,
                Avatar=u.Avatar
            }));
            return filter;
        }

        public async Task<List<UserViewModel>?> GetAllUsersAsync()
        => await _db.Users.Select(u=>new UserViewModel {
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
            Avatar = u.Avatar
        }).ToListAsync();

        public async Task<User?> GetbyMobileAndPassword(string mobile, string password)
        => await _db.Users.FirstOrDefaultAsync(u=>u.PhoneNumber == mobile && u.Password == password);

        public async Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.VerificationCode == verificationCode);

        public async Task<User?> GetByMobileAsync(string mobile)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile);

        public async Task<ClientSideUpdateUserViewModel?> GetUserForUpdateClientSide(int userId)
        => await _db.Users.Select(u => new ClientSideUpdateUserViewModel
        {
            Id = userId,
            FirstName=u.FirstName,
            LastName=u.LastName,
            //BirthDay = u.BirthDay.ToShamsi(),
            Avatar =u.Avatar,
            Gender=u.Gender,
            email=u.email,
            city=u.city,
            NationalCode=u.NationalCode
        }).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<UserClientSideView?> GetUserForViewClientSide(int userId)
        => await _db.Users.Where(u=>u.Id==userId).Select(u => new UserClientSideView
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
            Situation=u.Situation,
            Status=u.Status
        }).FirstAsync(u => u.Id == userId);

        public async Task<bool> MobileDuplicatedAsync(string mobile, int userId)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id!=userId);

    }
}
