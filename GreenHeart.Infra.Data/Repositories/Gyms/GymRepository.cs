using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Enums.Users;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.ViewModels.Gyms.Gyms;
using GreenHeart.Domain.ViewModels.Users.Users;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GreenHeart.Infra.Data.Repositories.Gyms
{
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        private readonly GreenHeartContext _db;
        public GymRepository(GreenHeartContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistConstantPhoneNumberAsync(string constantphone)
        => await _db.Gyms.AnyAsync(g => g.ConstantPhone== constantphone && g.IsDeleted == false);

        public async Task<bool> ExistConstantPhoneNumberAsync(string constantphone, int gymId)
        => await _db.Gyms.AnyAsync(g => g.ConstantPhone == constantphone&&g.Id!=gymId&&g.IsDeleted==false);

        public async Task<bool> ExistGymAsync(int gymId)
        => await _db.Gyms.AnyAsync(g=>g.Id==gymId&&g.IsDeleted==false);

        public async Task<bool> ExistSpecificSlug(string slug)
        =>await _db.Gyms.AnyAsync(u => u.Slug == slug && u.IsDeleted == false);

        public async Task<FilterGymViewModel> FilterGymAsync(FilterGymViewModel filter)
        {
            var query = _db.Gyms.AsQueryable();

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
                query = query.Where(r => r.Name.Contains(filter.Title));
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u=>new GymViewModel
            {
                Id = u.Id,
                Address = u.Address,
                ConstantPhone = u.ConstantPhone,
                ImageUrl = u.ImageUrl,
                IsDeleted = u.IsDeleted,
                CreatedDate = u.CreatedDate,
                Name = u.Name,
                Area = u.Area
            }));
            return filter;
        }

        public async Task<List<GymViewModel>?> GetAllGymItemsAsync()
        => await _db.Gyms.Where(g=>g.IsDeleted==false).Select(u=>new GymViewModel
        {
            Id=u.Id,
            Name=u.Name
        }).ToListAsync();

        public async Task<List<GymViewModel>?> GetAllGymsAsync()
        => await _db.Gyms.Select(u => new GymViewModel
        {
            Id = u.Id,
            Address = u.Address,
            ConstantPhone = u.ConstantPhone,
            ImageUrl = u.ImageUrl,
            IsDeleted = u.IsDeleted,
            CreatedDate = u.CreatedDate,
            Name = u.Name,
            Area = u.Area
        }).ToListAsync();

        public string GetGymName(int gymId)
        => _db.Gyms.Where(g => g.Id == gymId).Select(g => g.Name).First();

        //the below method will contoll of duplicating the slug and never gonna have similar slug
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
                    slug=slug.Substring(0,lastIndex + 1)+$"{Count}";
                }
            }            
            return slug ;
        }

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Gyms.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
