using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Records;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories
{
    public class ExperienceRepository : GenericRepository<Experience>, IExperienceRepository
    {
        private readonly HexagonContext _db;
        public ExperienceRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
