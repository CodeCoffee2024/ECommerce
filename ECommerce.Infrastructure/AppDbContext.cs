using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure
{
    public class AppDbContext : DbContext, IDbService
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserUserPermission> UserUserPermissions { get; set; }

        #endregion Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion Protected Methods
    }
}