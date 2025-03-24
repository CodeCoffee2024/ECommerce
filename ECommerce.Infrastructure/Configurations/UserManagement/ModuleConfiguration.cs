using ECommerce.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.UserManagement
{
    internal class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable(nameof(Module));

            // Primary Key
            builder.HasKey(module => module.Id);
            builder.Property(module => module.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(module => module.Name)
                   .HasMaxLength(50)
                   .IsRequired();
        }

        #endregion Public Methods
    }
}