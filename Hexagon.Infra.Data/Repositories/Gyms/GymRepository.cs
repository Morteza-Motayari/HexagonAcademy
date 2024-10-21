using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class GymRepository : GenericRepository<Gym>, IGymRepository
    {
        private readonly HexagonContext _db;
        public GymRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
