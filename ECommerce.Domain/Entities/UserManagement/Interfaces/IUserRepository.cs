namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IUserRepository
    {
        #region Public Methods

        Task<User?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        User FindByUsername(string username);

        void Add(User userPermission);

        #endregion Public Methods
    }
}