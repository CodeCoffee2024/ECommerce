namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IUserUserPermissionRepository
    {
        #region Public Methods

        IEnumerable<UserUserPermission> GetUserUserPermissionByUserId(Guid userId);

        void Remove(UserUserPermission userUserPermission);

        #endregion Public Methods
    }
}