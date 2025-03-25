using GreenHeart.Domain.Models.Banners;
using GreenHeart.Domain.Models.Common;
using GreenHeart.Domain.Models.Contact_Us;
using GreenHeart.Domain.Models.Gyms;
using GreenHeart.Domain.Models.KeyWords;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.Models.Records;
using GreenHeart.Domain.Models.Tickets;
using GreenHeart.Domain.Models.Users;
using GreenHeart.Domain.Models.Wallets;
using GreenHeart.Infra.Data.DataExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GreenHeart.Infra.Data.Context
{
    public class GreenHeartContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;

        public GreenHeartContext(DbContextOptions<GreenHeartContext> options, IHttpContextAccessor accessor) : base(options)
        {
            _accessor = accessor;
        }

        #region Banner
        public DbSet<Banner> Banners { get; set; }
        #endregion

        #region Contact Us
        public DbSet<ContactUs> ContactUs { get; set; }
        #endregion

        #region Gym
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymGallery> GymGalleries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportClass> SportClasses { get; set; }
        public DbSet<ClassComment> ClassComments { get; set; }
        public DbSet<ClassCommentReaction> ClassCommentReactions { get; set; }
        #endregion

        #region Links
        public DbSet<ClassUser> ClassUsers { get; set; }
        public DbSet<GymUser> GymUsers { get; set; }
        public DbSet<UserCertificates> UserCertificates { get; set; }
        public DbSet<GymStaff> GymStaffs { get; set; }
        #endregion

        #region Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<ClassOrder> ClassOrders { get; set; }
        #endregion

        #region Records
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        #endregion

        #region Tickets
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        #endregion

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission>? RolePermissions { get; set; }
        #endregion

        #region KeyWords
        public DbSet<KeyWord> KeyWords { get; set; }
        #endregion

        #region Wallets
        public DbSet<Wallet> Wallets { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
                base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                if (_accessor.CheckAuthentication())
                    entry.Entity.LastModifiedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value);

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    if (_accessor.CheckAuthentication())
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
                if (_accessor.CheckAuthentication())
                    entry.Entity.LastModifiedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value.ToString());

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    if (_accessor.CheckAuthentication())
                        entry.Entity.CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value.ToString());
                }
            }
            return base.SaveChanges();
        }
    }

}
