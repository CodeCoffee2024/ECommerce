using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermissionDropdown
{
    public sealed class GetUserPermissionDropdownResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }

        #endregion Properties

        #region Methods

        internal static GetUserPermissionDropdownResponse MapToResponse(UserPermission userPermission)
        {
            if (userPermission == null)
                throw new ArgumentNullException(nameof(userPermission));

            return new GetUserPermissionDropdownResponse()
            {
                Name = userPermission.Name,
                Id = userPermission.Id,
            };
        }

        #endregion Methods
    }
}