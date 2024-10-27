using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Users
{
    public class UserRoleRepository:GenericRepository<UserRole>,IUserRoleRepository
    {
        private readonly HexagonContext _db;
        public UserRoleRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }
    }
}
