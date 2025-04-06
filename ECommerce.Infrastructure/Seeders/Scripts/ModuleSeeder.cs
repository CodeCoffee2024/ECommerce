using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Infrastructure.Seeders.Scripts
{
    public class ModuleSeeder
    {
        #region Internal Methods

        internal static void Run(AppDbContext context)
        {
            var existingUserModule = context.Modules.FirstOrDefault(u => u.Name == "User");
            var existingUserPermissionModule = context.Modules.FirstOrDefault(u => u.Name == "UserPermission");
            var existingUnitOfMeasurementTypeModule = context.Modules.FirstOrDefault(u => u.Name == "UnifOfMeasurementType");
            var existingUnitOfMeasurementModule = context.Modules.FirstOrDefault(u => u.Name == "UnifOfMeasurement");
            if (existingUserModule == null)
            {
                var module = Module.Create("User", "Users", 2);
                context.Set<Module>().Add(module);
                context.SaveChanges();
            }
            if (existingUserPermissionModule == null)
            {
                var module = Module.Create("UserPermission", "User Permissions", 1);
                context.Set<Module>().Add(module);
                context.SaveChanges();
            }
            if (existingUnitOfMeasurementTypeModule == null)
            {
                var module = Module.Create("UnifOfMeasurementType", "Unit of Measurement Types", 3);
                context.Set<Module>().Add(module);
                context.SaveChanges();
            }
            if (existingUnitOfMeasurementModule == null)
            {
                var module = Module.Create("UnifOfMeasurement", "Unit of Measurements", 4);
                context.Set<Module>().Add(module);
                context.SaveChanges();
            }
        }

        #endregion Internal Methods
    }
}