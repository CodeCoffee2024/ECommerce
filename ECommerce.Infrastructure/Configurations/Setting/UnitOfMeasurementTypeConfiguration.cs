using ECommerce.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Setting
{
    public class UnitOfMeasurementTypeConfiguration : IEntityTypeConfiguration<UnitOfMeasurementType>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<UnitOfMeasurementType> builder)
        {
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ut => ut.Status)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(u => u.HasDecimal)
                .IsRequired();
        }

        #endregion Public Methods
    }
}