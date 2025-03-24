using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Dtos.Shared;
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetOneUserPermission
{
    public sealed class GetOneUserPermissionResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public string Permissions { get; set; } = string.Empty;
        public IEnumerable<ModulePermissionDTO> ModulePermissions { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentQueryResponse CreatedBy { get; set; } = new();
        public UserFragmentQueryResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetOneUserPermissionResponse MapToResponse(UserPermission userPermission, IEnumerable<ModulePermissionDTO> modulePermissions)
        {
            if (userPermission == null)
                throw new ArgumentNullException(nameof(userPermission));

            return new GetOneUserPermissionResponse()
            {
                ModulePermissions = modulePermissions,
                Name = userPermission.Name,
                Id = userPermission.Id,
                CreatedDate = userPermission.CreatedDate,
                ModifiedDate = userPermission.ModifiedDate,
                Permissions = userPermission.Permissions,
                CreatedBy = new UserFragmentQueryResponse()
                {
                    Id = userPermission.CreatedBy!.Id.ToString(),
                    Name = $"{userPermission.CreatedBy?.FirstName ?? "Unknown"} {userPermission.CreatedBy?.LastName ?? ""}".Trim()
                },
                ModifiedBy = new UserFragmentQueryResponse()
                {
                    Id = userPermission.CreatedBy!.Id.ToString(),
                    Name = $"{userPermission.ModifiedBy?.FirstName ?? "Unknown"} {userPermission.ModifiedBy?.LastName ?? ""}".Trim()
                }
            };
        }

        #endregion Methods
    }
}