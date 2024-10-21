using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;

namespace Hexagon.Infra.Data.Repositories
{
    public class StaffRepository: GenericRepository<Staff>, IStaffRepository
    {
        private readonly HexagonContext _db;
        public StaffRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }

    }
}
