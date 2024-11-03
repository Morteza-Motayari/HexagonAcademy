using Hexagon.Domain.Models.Gyms;
using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;
using Hexagon.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Context
{
    public class HexagonContext:DbContext
    {
        public HexagonContext(DbContextOptions<HexagonContext> options) : base(options)
        {

        }

        #region Gym
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymGallery> GymGalleries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportClass> SportClasses { get; set; }
        #endregion

        #region Links
        public DbSet<ClassUser> ClassUser { get; set; }
        public DbSet<GymUsers> GymUsers { get; set; }
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
    }
}
