using ECommerce.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Inventory
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ut => ut.Status)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(p => p.ParentProductCategory)
                  .WithMany()
                  .HasForeignKey(p => p.ParentProductCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.ParentProductCategoryId)
                  .IsRequired(false);

            builder.Property(u => u.IsSubCategory)
                .IsRequired();

            builder.HasMany(p => p.Subcategories)
                  .WithOne(p => p.ParentProductCategory)
                  .HasForeignKey(p => p.ParentProductCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        }

        #endregion Public Methods
    }
}