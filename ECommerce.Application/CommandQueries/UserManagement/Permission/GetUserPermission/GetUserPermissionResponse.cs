using ECommerce.Application.CommandQueries.Common;
using ECommerce.Domain.Entities.UserManagement;

namespace ECommerce.Application.CommandQueries.UserManagement.Permission.GetUserPermission
{
    public sealed class GetUserPermissionResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentQueryResponse CreatedBy { get; set; } = new();
        public UserFragmentQueryResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetUserPermissionResponse MapToResponse(UserPermission userPermission)
        {
            if (userPermission == null)
                throw new ArgumentNullException(nameof(userPermission));

            return new GetUserPermissionResponse()
            {
                Name = userPermission.Name,
                Id = userPermission.Id,
                CreatedDate = userPermission.CreatedDate,
                ModifiedDate = userPermission.ModifiedDate,
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