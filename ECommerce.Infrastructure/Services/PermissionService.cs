using ECommerce.Application.Abstractions;
using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Shared;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        #region Fields

        private IEnumerable<ModulePermissionDTO> ModulePermissions { get; set; }

        #endregion Fields

        #region Private Methods

        public PermissionService(IUserPermissionRepository userPermissionRepository)
        {
            var userField = typeof(Modules).GetField("User");
            var userDscription = userField.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var userModulePermission = new ModulePermissionDTO(
                Modules.User,
                userDscription,
                0,
                [
                    new PermissionDetailDTO(
                        Permissions.UserEnableToViewUser,
                        "View",
                        []
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToModifyUser,
                        "Modify",
                        [Permissions.UserEnableToViewUser]
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToDeleteUser,
                        "Delete",
                        [Permissions.UserEnableToViewUser]
                    ),
                ]
            );
            var userPermissionField = typeof(Modules).GetField("UserPermission");
            var userPermissionDscription = userPermissionField.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var userPermissionModulePermission = new ModulePermissionDTO(
                Modules.UserPermission,
                userPermissionDscription,
                1,
                [
                    new PermissionDetailDTO(
                        Permissions.UserEnableToViewUserPermission,
                        "View",
                        []
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToModifyUserPermission,
                        "Modify",
                        [Permissions.UserEnableToViewUserPermission]
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToDeleteUserPermission,
                        "Delete",
                        [Permissions.UserEnableToViewUserPermission]
                    ),
                ]
            );
            ModulePermissions = [
                userModulePermission,
                userPermissionModulePermission
            ];
        }

        public IEnumerable<ModulePermissionDTO> GetPermissions()
        {
            return ModulePermissions;
        }

        #endregion Private Methods
    }
}