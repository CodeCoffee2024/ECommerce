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

        public UserUserPermission()
        {
        }

        #endregion Public Constructors
    }
}