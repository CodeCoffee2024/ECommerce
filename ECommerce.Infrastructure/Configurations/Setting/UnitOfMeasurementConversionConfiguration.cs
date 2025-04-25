using ECommerce.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Setting
{
    public class UnitOfMeasurementConversionConfiguration : IEntityTypeConfiguration<UnitOfMeasurementConversion>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<UnitOfMeasurementConversion> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Value)
                .IsRequired()
                .HasPrecision(18, 6);

            builder.HasOne(u => u.ConvertFrom)
                  .WithMany(uom => uom.ConvertFroms)
                  .HasForeignKey(u => u.ConvertFromId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.ConvertTo)
                  .WithMany(uom => uom.ConvertTos)
                  .HasForeignKey(u => u.ConvertToId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UnitOfMeasurementConversions");
        }

        #endregion Public Methods
    }
}