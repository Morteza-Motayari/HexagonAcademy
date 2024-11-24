using Hexagon.Domain.Models.Common;
using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hexagon.Infra.Data.Context
{
    public class HexagonContext:DbContext
    {
        private readonly IHttpContextAccessor _accessor;

        public HexagonContext(DbContextOptions<HexagonContext> options, IHttpContextAccessor accessor ) : base(options)
        {
            _accessor = accessor;
        }

        #region Gym
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymGallery> GymGalleries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportClass> SportClasses { get; set; }
        #endregion

        #region Links
        public DbSet<ClassUser> ClassUsers { get; set; }
        public DbSet<GymUser> GymUsers { get; set; }
        public DbSet<UserCertificates> UserCertificates { get; set; }
        #endregion

        #region Records
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        #endregion

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission>? RolePermissions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy=int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);
                }
            }


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value.ToString());
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value.ToString());
                }
            }
            return base.SaveChanges();
        }
    }
}
