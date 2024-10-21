using Hexagon.Domain.Interfaces.Gyms;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Gyms
{
    public class SportClassRepository : GenericRepository<SportClass>, ISportClassRepository
    {
        private readonly HexagonContext _db;
        public SportClassRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
