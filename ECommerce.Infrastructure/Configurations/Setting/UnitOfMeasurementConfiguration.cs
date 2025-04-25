using ECommerce.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Setting
{
    public class UnitOfMeasurementConfiguration : IEntityTypeConfiguration<UnitOfMeasurement>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<UnitOfMeasurement> builder)
        {
            builder.HasKey(u => u.Id); // Primary Key

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Abbreviation)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(u => u.Status)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(u => u.UnitOfMeasurementType)
                .WithMany(ut => ut.UnitOfMeasurements)
                .HasForeignKey(u => u.UnitOfMeasurementTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion Public Methods
    }
}