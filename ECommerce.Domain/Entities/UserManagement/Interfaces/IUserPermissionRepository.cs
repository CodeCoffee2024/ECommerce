using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IUserPermissionRepository
    {
        #region Public Methods

        Task<UserPermission?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<IEnumerable<UserPermission>> GetAllPermissions(CancellationToken cancellationToken);

        Task<PagedResult<UserPermission>> GetListingPageResultAsync(DefaultFilterBaseDto searchValue, CancellationToken cancellationToken);

        Task<PagedResult<UserPermission>> GetListingPageDropdownResultAsync(DefaultFilterBaseDto searchValue, CancellationToken cancellationToken);

        UserPermission FindByName(string name);

        Task<UnpagedResult<UserPermission>> GetListingPageResultExportAsync(DefaultFilterBaseDto searchValue, CancellationToken cancellationToken);

        void Add(UserPermission userPermission);

        void Remove(UserPermission userPermission);

        void Update(UserPermission userPermission);

        #endregion Public Methods
    }
}