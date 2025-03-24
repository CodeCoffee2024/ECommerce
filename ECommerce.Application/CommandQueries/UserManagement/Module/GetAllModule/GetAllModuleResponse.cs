using ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission;
using ECommerce.Domain.Dtos.Shared;

namespace ECommerce.Application.CommandQueries.UserManagement.Module.GetAllUserPermission
{
    public sealed class GetAllModuleResponse
    {
        #region Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }
        public List<UserPermissionFragmentDTO> Permissions { get; set; }

        internal static GetAllModuleResponse MapToResponse(ModulePermissionDTO module)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));

            return new GetAllModuleResponse()
            {
                Name = module.Name,
                Permissions = module.Permissions
                .Select(up => new UserPermissionFragmentDTO
                {
                    Name = up.Permission,
                    Description = up.Description,
                    Dependencies = string.Join(",", up.Dependencies),
                    Permissions = up.Permission
                })
                .ToList(),
                Order = module.Order,
                Description = module.Description,
            };
        }

        #endregion Properties
    }
}