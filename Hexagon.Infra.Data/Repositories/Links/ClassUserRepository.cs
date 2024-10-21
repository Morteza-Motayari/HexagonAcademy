using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class ClassUserRepository : GenericRepository<ClassUser>, IClassUserRepository
    {
        private readonly HexagonContext _db;
        public ClassUserRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
