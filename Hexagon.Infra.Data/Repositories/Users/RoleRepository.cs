using Hexagon.Domain.Enums.Filter;
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
        => await _db.Roles.AnyAsync(u => u.RoleTitle == roleTilte&&u.IsDeleted==false);

        public async Task<FilterRoleViewModel> FilteRolesAsync(FilterRoleViewModel filter)
        {
            var query = _db.Roles.AsQueryable();

            #region Filter Search
            switch (filter.Status)
            {
                case ExistingStatus.All:
                    break;
                case ExistingStatus.Deleted:
                    query = query.Where(u => u.IsDeleted == true);
                    break;
                case ExistingStatus.NotDeleted:
                    query = query.Where(u => !u.IsDeleted);
                    break;
            }

            if (filter.Title != null)
            {
                query = query.Where(r => r.RoleTitle.Contains(filter.Title));
            }
            #endregion

            query=query.OrderByDescending(u=>u.CreatedDate);

            await filter.Paging(query.Select(r => new RoleViewModel
            {
                Id = r.Id,
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
                CreatedDate = u.CreatedDate
            }).ToListAsync();
            return roles;
        }

        public async Task<List<Role>?> GetUserRoles(List<int>? ids)
        {
            if (ids == null || ids.Count == 0)
                return null;
            List<Role> roles = new();
            foreach(var item in ids)
            {
                roles.Add(await GetByIdAsync(item));
            }
            return roles;
        }
    }
}
