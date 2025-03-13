using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.UserManagement
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            // Primary Key
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.HasIndex(user => user.LastName)
                   .IsUnique(true);

            builder.Property(user => user.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(user => user.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(user => user.LastName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(user => user.MiddleName)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(user => user.Username)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(user => user.Password)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.Property(user => user.BirthDate)
                   .IsRequired(false);

            builder.HasOne(user => user.CreatedBy)
                   .WithMany()
                   .HasForeignKey(user => user.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(user => user.ModifiedBy)
                   .WithMany()
                   .HasForeignKey(user => user.ModifiedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        #endregion Public Methods
    }
}