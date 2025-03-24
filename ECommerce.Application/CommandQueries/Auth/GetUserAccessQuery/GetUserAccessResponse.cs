using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.CommandQueries.Auth.GetUserAccessQuery
{
    public sealed class GetUserAccessResponse
    {
        #region Properties

        public string Permissions { get; set; }

        internal static GetUserAccessResponse MapToResponse(ICollection<UserUserPermission> userPermission)
        {
            return new GetUserAccessResponse()
            {
                Permissions = string.Join(",", userPermission.Select(it => it.UserPermission.Permissions))
            };
        }

        #endregion Properties
    }
}