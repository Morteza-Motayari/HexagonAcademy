using Hexagon.Domain.Interfaces.Links;
using Hexagon.Domain.Models.Links;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories.Links
{
    public class UserCertificatesRepository : GenericRepository<UserCertificates>, IUserCertificatesRepository
    {
        private readonly HexagonContext _db;
        public UserCertificatesRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
