using ECommerce.Application.Abstractions;
using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Shared;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Fields

        #region Public Constructors

        public PermissionService(IUserPermissionRepository userPermissionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
            var unitOfMeasurementField = typeof(Modules).GetField("UnitOfMeasurement");
            var unitOfMeasurementDscription = unitOfMeasurementField.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var unitOfMeasurementModulePermission = new ModulePermissionDTO(
                Modules.UnitOfMeasurement,
                unitOfMeasurementDscription,
                1,
                [
                    new PermissionDetailDTO(
                        Permissions.UserEnableToViewUnitOfMeasurement,
                        "View",
                        []
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToModifyUnitOfMeasurement,
                        "Modify",
                        [Permissions.UserEnableToViewUnitOfMeasurement]
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToDeleteUnitOfMeasurement,
                        "Delete",
                        [Permissions.UserEnableToViewUnitOfMeasurement]
                    ),
                ]
            );
            var unitOfMeasurementTypeField = typeof(Modules).GetField("UnitOfMeasurementType");
            var unitOfMeasurementTypeDscription = unitOfMeasurementTypeField.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var unitOfMeasurementTypeModulePermission = new ModulePermissionDTO(
                Modules.UnitOfMeasurementType,
                unitOfMeasurementTypeDscription,
                1,
                [
                    new PermissionDetailDTO(
                        Permissions.UserEnableToViewUnitOfMeasurementType,
                        "View",
                        []
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToModifyUnitOfMeasurementType,
                        "Modify",
                        [Permissions.UserEnableToViewUnitOfMeasurementType]
                    ),
                    new PermissionDetailDTO(
                        Permissions.UserEnableToDeleteUnitOfMeasurementType,
                        "Delete",
                        [Permissions.UserEnableToViewUnitOfMeasurementType]
                    ),
                ]
            );
            ModulePermissions = [
                userModulePermission,
                userPermissionModulePermission,
                unitOfMeasurementModulePermission,
                unitOfMeasurementTypeModulePermission,
            ];
        }

        #endregion Public Constructors

        #region Properties

        private IEnumerable<ModulePermissionDTO> ModulePermissions { get; set; }

        #endregion Properties

        #region Public Methods

        public IEnumerable<ModulePermissionDTO> GetPermissions()
        {
            return ModulePermissions;
        }

        public bool HasPermission(params string[] requiredPermissions)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity is null || !user.Identity.IsAuthenticated)
                return false;

            var userPermissions = user.Claims
                .Where(c => c.Type == "Permissions")
                .SelectMany(c => c.Value.Split(","))
                .Select(p => p.Trim())
                .Distinct()
                .ToList();

            return requiredPermissions.All(p => userPermissions.Contains(p));
        }

        #endregion Public Methods
    }
}