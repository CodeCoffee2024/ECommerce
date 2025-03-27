using ECommerce.Domain.Entities.UserManagement;
using System.ComponentModel;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.ExportUserPermission
{
    public sealed class ExportUserPermissionResponse
    {
        #region Properties

        [Description("Name")]
        public string Name { get; set; } = string.Empty;

        [Description("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Description("Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        #endregion Properties

        #region Methods

        internal static ExportUserPermissionResponse MapToResponse(UserPermission userPermission)
        {
            if (userPermission == null)
                throw new ArgumentNullException(nameof(userPermission));

            return new ExportUserPermissionResponse()
            {
                Name = userPermission.Name,
                CreatedDate = userPermission.CreatedDate,
                CreatedBy = $"{userPermission.CreatedBy?.FirstName ?? "Unknown"}  {userPermission.CreatedBy?.LastName ?? ""}".Trim(),
            };
        }

        #endregion Methods
    }
}