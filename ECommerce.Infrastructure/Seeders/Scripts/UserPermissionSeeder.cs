using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Infrastructure.Seeders.Scripts
{
    public class UserPermissionSeeder
    {
        #region Internal Methods

        internal static void Run(AppDbContext context)
        {
            var existingPermissionSuperAdmin = context.UserPermissions.FirstOrDefault(u => u.Name == "Super Admin");
            if (existingPermissionSuperAdmin == null)
            {
                var superadmin = context.Set<User>().FirstOrDefault(u => u.Email == "admin@gmail.com");
                var userPermissionSuperAdmin = UserPermission.Create("*", "Super Admin", superadmin?.Id, DateTime.UtcNow);
                context.Set<UserPermission>().Add(userPermissionSuperAdmin);
                context.SaveChanges();
            }
        }

        #endregion Internal Methods
    }
}