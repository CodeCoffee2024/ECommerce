using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.UserManagement
{
    internal class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable(nameof(UserPermission));

            builder.HasKey(userPermission => userPermission.Id);
            builder.Property(userPermission => userPermission.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(userPermission => userPermission.CreatedBy)
                   .WithMany()
                   .HasForeignKey(userPermission => userPermission.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(userPermission => userPermission.ModifiedBy)
                   .WithMany()
                   .HasForeignKey(userPermission => userPermission.ModifiedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        #endregion Public Methods
    }
}