using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.UserManagement.User;

namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IUserRepository
    {
        #region Public Methods

        Task<User?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<PagedResult<User>> GetListingPageResultAsync(UserListingDTO searchValue, CancellationToken cancellationToken);

        Task<UnpagedResult<User>> GetListingPageResultExportAsync(UserListingDTO searchValue, CancellationToken cancellationToken);

        User? GetUserPermission(Guid Id);

        User FindByUsername(string username);

        User FindByEmail(string email);

        void Add(User user);

        void Remove(User user);

        void Update(User user);

        #endregion Public Methods
    }
}