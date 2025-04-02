using ECommerce.Domain.Entities.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ECommerce.Infrastructure.Configurations.Log
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // Since it's a GUID, no identity auto-generation

            builder.Property(x => x.PrimaryKey)
                   .IsRequired(false); // Nullable primary key

            builder.Property(x => x.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.EventType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Timestamp)
                   .IsRequired();

            // Store OldValues and NewValues as JSON
            builder.Property(x => x.OldValues)
                   .HasConversion(
                        v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => v == null ? null : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null)
                   )
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.NewValues)
                   .HasConversion(
                        v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => v == null ? null : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null)
                   )
                   .HasColumnType("nvarchar(max)");
        }

        #endregion Public Methods
    }
}