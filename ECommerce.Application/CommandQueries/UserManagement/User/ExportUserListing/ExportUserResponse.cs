using System.ComponentModel;

using ECommerce.Application.Common.Helpers;

namespace ECommerce.Application.CommandQueries.UserManagement.User.ExportUserListing
{
    public sealed class ExportUserResponse
    {
        #region Properties

        [Description("Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Description("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Description("Last Name")]
        public string MiddleName { get; set; } = string.Empty;

        [Description("User Permissions")]
        public string UserPermissions { get; set; } = string.Empty;

        [Description("Created Date")]
        public string? CreatedDate { get; set; }

        [Description("Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        #endregion Properties

        #region Methods

        internal static ExportUserResponse MapToResponse(ECommerce.Domain.Entities.UserManagement.User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new ExportUserResponse()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName ?? "--",
                UserPermissions = string.Join(",", user.UserUserPermissions!.Select(it => it.UserPermission.Name)),
                CreatedDate = DateHelper.ToFormattedDate(user.CreatedDate!.Value),
                CreatedBy = $"{user.CreatedBy?.FirstName ?? "Unknown"}  {user.CreatedBy?.LastName ?? ""}".Trim(),
            };
        }

        #endregion Methods
    }
}