using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Infrastructure.Seeders.Scripts
{
    public class UserPermissionSeeder
    {
        #region Internal Methods

        internal static void Run(AppDbContext context)
        {
            var existingPermissionSuperAdmin = context.UserPermissions.FirstOrDefault(u => u.Name == "User Access");
            var superadmin = context.Set<User>().FirstOrDefault(u => u.Email == "admin@gmail.com");
            if (existingPermissionSuperAdmin == null)
            {
                var userPermissionSuperAdmin = UserPermission.Create(
                    Permissions.UserEnableToViewUser + "," +
                    Permissions.UserEnableToModifyUser + "," +
                    Permissions.UserEnableToDeleteUser +
                    "", "User Access", superadmin?.Id, DateTime.UtcNow);
                context.Set<UserPermission>().Add(userPermissionSuperAdmin);

                userPermissionSuperAdmin = UserPermission.Create(
                    Permissions.UserEnableToViewUserPermission + "," +
                    Permissions.UserEnableToModifyUserPermission + "," +
                    Permissions.UserEnableToDeleteUserPermission +
                    "", "User Permission Access", superadmin?.Id, DateTime.UtcNow);
                context.Set<UserPermission>().Add(userPermissionSuperAdmin);

                userPermissionSuperAdmin = UserPermission.Create(
                    Permissions.UserEnableToViewUnitOfMeasurement + "," +
                    Permissions.UserEnableToModifyUnitOfMeasurement + "," +
                    Permissions.UserEnableToDeleteUnitOfMeasurement +
                    "", "Unif of Measurement Access", superadmin?.Id, DateTime.UtcNow);
                context.Set<UserPermission>().Add(userPermissionSuperAdmin);

                userPermissionSuperAdmin = UserPermission.Create(
                    Permissions.UserEnableToViewUnitOfMeasurementType + "," +
                    Permissions.UserEnableToModifyUnitOfMeasurementType + "," +
                    Permissions.UserEnableToDeleteUnitOfMeasurementType +
                    "", "Unif of Measurement Access", superadmin?.Id, DateTime.UtcNow);
                context.Set<UserPermission>().Add(userPermissionSuperAdmin);

                context.SaveChanges();
            }
            existingPermissionSuperAdmin = context.UserPermissions.FirstOrDefault(u => u.Name == "User Permission Access");
            var existingUserAccess = context.UserUserPermissions.FirstOrDefault(u => u.UserId == superadmin!.Id && u.UserPermissionId == existingPermissionSuperAdmin.Id);
            if (existingUserAccess == null)
            {
                var userUserPermission = UserUserPermission.Create(superadmin!.Id!.Value, existingPermissionSuperAdmin!.Id!.Value);
                context.Set<UserUserPermission>().Add(userUserPermission);
                existingPermissionSuperAdmin = context.UserPermissions.FirstOrDefault(u => u.Name == "User Access");
                userUserPermission = UserUserPermission.Create(superadmin!.Id!.Value, existingPermissionSuperAdmin!.Id!.Value);
                context.Set<UserUserPermission>().Add(userUserPermission);
                context.SaveChanges();
            }

            //staff
            var staff = context.Set<User>().FirstOrDefault(u => u.Email == "staff@gmail.com");
            existingPermissionSuperAdmin = context.UserPermissions.FirstOrDefault(u => u.Name == "User Permission Access");
            var existingStaffAccess = context.UserUserPermissions.FirstOrDefault(u => u.UserId == staff!.Id && u.UserPermissionId == existingPermissionSuperAdmin.Id);
            if (existingStaffAccess == null)
            {
                var existingPermissionStaff = context.UserPermissions.FirstOrDefault(u => u.Name == "User Permission Access");
                var userUserPermission = UserUserPermission.Create(staff!.Id!.Value, existingPermissionStaff!.Id!.Value);
                context.Set<UserUserPermission>().Add(userUserPermission);
                context.SaveChanges();
            }
        }

        #endregion Internal Methods
    }
}