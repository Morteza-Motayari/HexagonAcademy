using Hexagon.Domain.Interfaces.Users;
using Hexagon.Domain.Models.Users;
using Hexagon.Domain.ViewModels.Users.Roles;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Infra.Data.Repositories.Users
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly HexagonContext _db;
        public RoleRepository(HexagonContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> ExistRoleTitle(string roleTilte)
        => await _db.Roles.AnyAsync(u => u.RoleTitle == roleTilte);

        public async Task<FilterRoleViewModel> FilteRolesAsync(FilterRoleViewModel filter)
        {
            var query = _db.Roles.AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case FilterRoleStatus.All:
                    break;
                case FilterRoleStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case FilterRoleStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }

            if (filter.Title != null)
            {
                query = query.Where(r => r.RoleTitle.Contains(filter.Title) || r.RoleName.Contains(filter.Title)).Distinct();
            }
            #endregion

            query=query.OrderByDescending(u=>u.CreatedDate);

            await filter.Paging(query.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.RoleName,
                RoleTitle = r.RoleTitle,
                CreatedDate = r.CreatedDate,
                IsDeleted = r.IsDeleted
            }));
            return filter;
        }

        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = await _db.Roles.Select(u => new RoleViewModel
            {
                Id = u.Id,
                RoleTitle = u.RoleTitle,
                RoleName = u.RoleName,
                CreatedDate = u.CreatedDate
            }).ToListAsync();
            return roles;
        }
    }
}
