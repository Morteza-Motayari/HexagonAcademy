using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class GymGalleryRepository : GenericRepository<GymGallery>,IGymGalleryRepository
    {
        private readonly HexagonContext _db;

        public GymGalleryRepository(HexagonContext db):base(db)
        {
            _db = db;
        }
    }
}
