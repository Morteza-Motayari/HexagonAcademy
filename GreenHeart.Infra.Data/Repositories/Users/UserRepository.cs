using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Users;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.ViewModels.Users.Roles;
using GreenHeart.Domain.ViewModels.Users.Users;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using GreenHeart.Infra.Data.DataExtensions;
using GreenHeart.Domain.Models.Records;
using System.Collections.ObjectModel;
using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.ViewModels.Users.Staffs.Trainers;
using GreenHeart.Domain.ViewModels.Users.Staffs.Caders;

namespace GreenHeart.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly GreenHeartContext _db;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRepository(GreenHeartContext db, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository) : base(db)
        {
            _db = db;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> ExistMobileAsync(string mobile)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.IsDeleted == false);

        public async Task<bool> ExistMobileAsync(string mobile, int id)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id != id && u.IsDeleted == false);

        public async Task<bool> ExistSpecificSlug(string slug)
        => await _db.SportClasses.AnyAsync(u => u.Slug == slug && u.IsDeleted == false);

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
                        query = query.Where(u => u.Situation == UserSituation.Cader);
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
            var data = _db.Users.Where(u => u.Status == UserStatus.Active && u.IsDeleted == false)
                .Where(a => a.FirstName.Contains(term)
            || a.LastName.Contains(term)
            || a.PhoneNumber.Contains(term)).Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Gender = u.Gender,
                Avatar = u.Avatar
            }).ToList().AsReadOnly();

            return data;
        }

        public async Task<User?> GetbyMobileAndPassword(string mobile, string password)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.Password == password && u.IsDeleted == false);

        public async Task<User?> GetByMobileAndVerificationCodeAsync(string mobile, string verificationCode)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.VerificationCode == verificationCode && u.IsDeleted == false);

        public async Task<User?> GetByMobileAsync(string mobile)
        => await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == mobile && u.IsDeleted == false);

        public async Task<string?> GetJustAvatarAsync(int? userId)
        =>await _db.Users.Where(u=>u.Id==userId).Select(u=>u.Avatar).FirstAsync();

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

        public async Task<UserGender?> GetUserGenderAsync(int userId)
        => await _db.Users.Where(u => u.Id == userId)
            .Select(u =>(UserGender)u.Gender).FirstAsync();

        public async Task<User?> GetUserWithChilds(int userId)
        => await _db.Users.Include(u => u.staffes).Include(u=>u.userRoles).Include(u => u.UserCertificates).FirstOrDefaultAsync(u=>u.Id==userId);

        public async Task<bool> MobileDuplicatedAsync(string mobile, int userId)
        => await _db.Users.AnyAsync(u => u.PhoneNumber == mobile && u.Id != userId && u.IsDeleted == false);

        public async Task<string> PutSpecificSlug(string slug)
        {
            slug = slug + $"-{1}";
            if (await ExistSpecificSlug(slug))
            {
                int lastIndex = slug.LastIndexOf('-');
                int Count = int.Parse(slug.Substring(lastIndex + 1));
                while (await ExistSpecificSlug(slug))
                {
                    Count++;
                    slug = slug.Substring(0, lastIndex + 1) + $"{Count}";
                }
            }
            return slug;
        }

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Users.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<string?> GetUserAvatar(int userId)
        => await _db.Users.Where(u=>u.Id==userId).Select(u=>u.Avatar).FirstAsync();

        public async Task<ClientSideFilterTrainerViewModel> ClientSideFilterTrainer(ClientSideFilterTrainerViewModel filter)
        {
            var query = _db.Users.Include(t=>t.staffes).ThenInclude(t=>t.UserCertificates).Include(t=>t.UserCertificates)
                .Where(u=>!u.IsDeleted&&u.staffes.Where(t=>!t.IsDeleted&&t.UserCertificates.Count()>0).Any()).AsQueryable();


            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ClientSideTrainerViewModel
            {
                Slug=u.Slug,
                Avatar=u.Avatar,
                email=u.email,
                FullName=u.FirstName+" "+u.LastName,
                Position=u.staffes.Select(t=>t.Position).First()
            }));
            return filter;
        }

        public async Task<List<ClientSideTrainerViewModel>?> GetTrainersForHomePage()
        =>await _db.Users.Include(t => t.staffes).ThenInclude(t => t.UserCertificates).Include(t => t.UserCertificates)
                .Where(u => !u.IsDeleted && u.staffes.Where(t => !t.IsDeleted && t.UserCertificates.Count() > 0).Any())
                .OrderBy(r => Guid.NewGuid()).Take(6).Select(t => new ClientSideTrainerViewModel
                {
                    Avatar = t.Avatar,
                    email = t.email,
                    FullName = t.FirstName + " " + t.LastName,
                    Slug = t.Slug,
                    Position = t.staffes.Where(s => !s.IsDeleted && s.UserCertificates.Count() > 0).Select(t => t.Position).First()
                }).ToListAsync();

        public async Task<User?> GetUserBySlug(string slug)
        =>await _db.Users.Where(s=>s.Slug==slug&&s.IsDeleted==false)
            .Select(u=>new User
            {
                Id= u.Id,
                Avatar=u.Avatar,
                FirstName=u.FirstName,
                LastName=u.LastName,
                email=u.email,
                Slug=u.Slug
            }).FirstOrDefaultAsync();

        public async Task<List<ClientSideCaderViewModel>?> GetCadersForAbouUsPage()
        =>await _db.Users.Include(t => t.staffes).ThenInclude(t => t.userRoles).Include(t => t.userRoles)
                .Where(u => !u.IsDeleted && u.staffes.Where(t => !t.IsDeleted && t.userRoles.Count() > 0).Any())
                .OrderBy(r => Guid.NewGuid()).Take(3).Select(t => new ClientSideCaderViewModel
                {
                    Avatar = t.Avatar,
                    email = t.email,
                    FullName = t.FirstName + " " + t.LastName,
                    Position = t.staffes.Where(s=>!s.IsDeleted&&s.userRoles.Count()>0).Select(t => t.Position).First()
                }).ToListAsync();

        public async Task<string?> GetStaffSlugByIdAsync(int userId)
        => await _db.Users.Where(u => u.Id == userId)
            .Select(s => s.Slug).FirstOrDefaultAsync();
    }
}
