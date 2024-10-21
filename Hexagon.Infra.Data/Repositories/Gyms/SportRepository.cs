using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class SportRepository : GenericRepository<Sport>, ISportRepository
    {
        private readonly HexagonContext _db;
        public SportRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
