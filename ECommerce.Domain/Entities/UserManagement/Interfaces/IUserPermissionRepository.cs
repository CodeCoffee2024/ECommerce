namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IUserPermissionRepository
    {
        #region Public Methods

        Task<UserPermission?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        void Add(UserPermission userPermission);

        #endregion Public Methods
    }
}