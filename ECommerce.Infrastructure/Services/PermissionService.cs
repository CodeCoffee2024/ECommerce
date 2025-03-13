using ECommerce.Application.Abstractions;
using ECommerce.Domain.Commons.Constants;
using ECommerce.Domain.Dtos.Shared;
using ECommerce.Domain.Enums;

namespace ECommerce.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        #region Private Methods

        public IEnumerable<ModulePermissionDTO> GetPermissions()
        {
            return PermissionConstants.ModulePermissions.Select(module =>
                new ModulePermissionDTO
                {
                    Module = module.Key.ToString(),
                    Description = module.Key.GetDescription(),
                    Permissions = module.Value.Select(permission =>
                        new PermissionDetailDTO
                        {
                            Permission = permission.Key.ToString(),
                            Description = permission.Key.GetDescription(),
                            Dependencies = permission.Value
                        })
                });
        }

        public IEnumerable<string> GetDependencies(PermissionAccess permission)
        {
            return PermissionConstants.ModulePermissions.Values
                .SelectMany(p => p)
                .Where(p => p.Key == permission)
                .SelectMany(p => p.Value)
                .Distinct();
        }

        // Check if the user has access to the specific module and permission
        public bool HasAccess(string module, string permission, IEnumerable<string> userPermissions)
        {
            var hasModuleAccess = PermissionConstants.ModulePermissions
                .Any(m => m.Key.ToString().Equals(module, StringComparison.OrdinalIgnoreCase) &&
                          m.Value.Any(p => p.Key.ToString().Equals(permission, StringComparison.OrdinalIgnoreCase)));

            if (!hasModuleAccess)
                return false;

            var dependencies = PermissionConstants.ModulePermissions
                .Where(m => m.Key.ToString().Equals(module, StringComparison.OrdinalIgnoreCase))
                .SelectMany(m => m.Value)
                .Where(p => p.Key.ToString().Equals(permission, StringComparison.OrdinalIgnoreCase))
                .SelectMany(p => p.Value)
                .ToList();

            return userPermissions.Contains($"{module}.{permission}") &&
                   dependencies.All(dep => userPermissions.Contains($"{module}.{dep}"));
        }

        #endregion Private Methods
    }
}