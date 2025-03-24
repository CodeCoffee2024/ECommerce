namespace ECommerce.Domain.Entities.UserManagement
{
    public class UserUserPermission
    {
        #region Properties

        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public Guid UserPermissionId { get; set; }
        public virtual UserPermission UserPermission { get; set; } = null!;

        #endregion Properties

        #region Public Constructors

        private UserUserPermission(Guid userId, Guid userPermissionId)
        {
            UserId = userId;
            UserPermissionId = userPermissionId;
        }
        public UserUserPermission()
        {
        }

        public static UserUserPermission Create(Guid userId, Guid userPermissionId)
        {
            var userUserPermission = new UserUserPermission(userId, userPermissionId);
            return userUserPermission;
        }

        #endregion Public Constructors
    }
}