using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Log;
using ECommerce.Domain.Entities.Settings;
using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

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
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public DbSet<UnitOfMeasurementConversion> UnitOfMeasurementConversions { get; set; }
        public DbSet<UnitOfMeasurementType> UnitOfMeasurementTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<UserUserPermission> UserUserPermissions { get; set; }

        public EntityEntry Entry(object entity) => base.Entry(entity); // ✅ Correct implementation

        #endregion Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            var dictionaryComparer = new ValueComparer<Dictionary<string, string>>(
                (c1, c2) => JsonConvert.SerializeObject(c1) == JsonConvert.SerializeObject(c2), // Compare JSON representation
                c => c == null ? 0 : JsonConvert.SerializeObject(c).GetHashCode(),
                c => c == null ? new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(c))
            );

            modelBuilder.Entity<ActivityLog>()
                .Property(e => e.OldValues)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v), // Convert to JSON string
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                )
                .Metadata.SetValueComparer(dictionaryComparer);

            modelBuilder.Entity<ActivityLog>()
                .Property(e => e.NewValues)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                )
                .Metadata.SetValueComparer(dictionaryComparer);
        }

        #endregion Protected Methods
    }
}