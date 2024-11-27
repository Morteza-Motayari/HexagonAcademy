using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.ViewModels.Gyms.GymGalleries;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class GymGalleryRepository : GenericRepository<GymGallery>,IGymGalleryRepository
    {
        private readonly HexagonContext _db;

        public GymGalleryRepository(HexagonContext db):base(db)
        {
            _db = db;
        }

        public async Task<List<GymGalleryViewModel>> GetGymGalleryAsync(int gymId)
        =>await _db.GymGalleries.Where(g=>g.GymId==gymId).Select(u=>new GymGalleryViewModel
        {
            Id = u.Id,
            GymId = u.GymId,
            CreatedDate = DateTime.Now,
            ImageUrl = u.ImageUrl,
            ImageTitle = u.ImageTitle
        }).ToListAsync();

        public async Task<int> GymGalleryCountImagesAsync(int gymId)
        => await _db.GymGalleries.CountAsync(g => g.GymId == gymId);
    }
}
