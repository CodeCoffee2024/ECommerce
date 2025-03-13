using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.UserManagement
{
    internal class UserUserPermissionConfiguration : IEntityTypeConfiguration<UserUserPermission>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<UserUserPermission> builder)
        {
            builder.ToTable(nameof(UserUserPermission));

            builder.HasKey(uup => new { uup.UserId, uup.UserPermissionId });

            builder.HasOne(uup => uup.User)
                   .WithMany(u => u.UserUserPermissions)
                   .HasForeignKey(uup => uup.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uup => uup.UserPermission)
                   .WithMany(up => up.UserUserPermissions)
                   .HasForeignKey(uup => uup.UserPermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion Public Methods
    }
}