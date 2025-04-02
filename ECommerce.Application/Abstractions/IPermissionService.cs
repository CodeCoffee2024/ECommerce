using ECommerce.Domain.Dtos.Shared;

namespace ECommerce.Application.Abstractions
{
    public interface IPermissionService
    {
        #region Public Methods

        IEnumerable<ModulePermissionDTO> GetPermissions();

        //IEnumerable<string> GetDependencies(PermissionAccess permission);

        //bool HasAccess(string module, string permission, IEnumerable<string> userPermissions);
        bool HasPermission(params string[] requiredPermissions);

        #endregion Public Methods
    }
}