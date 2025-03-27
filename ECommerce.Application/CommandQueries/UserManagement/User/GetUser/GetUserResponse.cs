using ECommerce.Application.CommandQueries.Common;

namespace ECommerce.Application.CommandQueries.UserManagement.User.GetUser
{
    public sealed class GetUserResponse
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentQueryResponse CreatedBy { get; set; } = new();
        public UserFragmentQueryResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetUserResponse MapToResponse(ECommerce.Domain.Entities.UserManagement.User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return new GetUserResponse()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Id = user.Id,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate,
                Permissions = string.Join(", ", user.UserUserPermissions.Select(it => it.UserPermission.Name)),
                CreatedBy = new UserFragmentQueryResponse()
                {
                    Id = user.CreatedBy!?.Id.ToString(),
                    Name = $"{user.CreatedBy?.FirstName ?? "Unknown"} {user.CreatedBy?.LastName ?? ""}".Trim()
                },
                ModifiedBy = new UserFragmentQueryResponse()
                {
                    Id = user.CreatedBy!?.Id.ToString(),
                    Name = $"{user.ModifiedBy?.FirstName ?? "Unknown"} {user.ModifiedBy?.LastName ?? ""}".Trim()
                }
            };
        }

        #endregion Methods
    }
}