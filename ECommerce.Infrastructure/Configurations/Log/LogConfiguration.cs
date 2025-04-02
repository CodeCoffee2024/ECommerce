using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Log
{
    public class LogConfiguration : IEntityTypeConfiguration<ECommerce.Domain.Entities.Log.Log>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ECommerce.Domain.Entities.Log.Log> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // Auto-increment primary key

            builder.Property(x => x.Timestamp)
                   .IsRequired();

            builder.Property(x => x.Level)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Message)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)"); // To allow long log messages

            builder.Property(x => x.Exception)
                   .HasColumnType("nvarchar(max)"); // Nullable field for exception details
        }

        #endregion Public Methods
    }
}