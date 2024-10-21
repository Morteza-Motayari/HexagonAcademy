using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class GymUsersRepository : GenericRepository<GymUsers>, IGymUsersRepository
    {
        private readonly HexagonContext _db;
        public GymUsersRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
