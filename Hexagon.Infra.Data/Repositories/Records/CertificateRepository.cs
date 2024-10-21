using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Records;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories
{
    public class CertificateRepository : GenericRepository<Certificate>, ICertificateRepository
    {
        private readonly HexagonContext _db;
        public CertificateRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
