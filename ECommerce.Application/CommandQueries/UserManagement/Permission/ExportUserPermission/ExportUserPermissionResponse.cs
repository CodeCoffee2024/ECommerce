using System.ComponentModel;

using ECommerce.Application.Common.Helpers;
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.ExportUserPermission
{
    public sealed class ExportUserPermissionResponse
    {
        #region Properties

        [Description("Name")]
        public string Name { get; set; } = string.Empty;

        [Description("Created Date")]
        public string? CreatedDate { get; set; }

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
                CreatedDate = DateHelper.ToFormattedDate(userPermission.CreatedDate.Value),
                CreatedBy = $"{userPermission.CreatedBy?.FirstName ?? "Unknown"}  {userPermission.CreatedBy?.LastName ?? ""}".Trim(),
            };
        }

        #endregion Methods
    }
}