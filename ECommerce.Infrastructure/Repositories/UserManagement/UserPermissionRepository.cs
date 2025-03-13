using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Infrastructure.Repositories.UserManagement
{
    internal sealed class UserPermissionRepository : RepositoryBase<UserPermission>, IUserPermissionRepository
    {
        #region Public Constructors

        public UserPermissionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Private Methods

        public void Create(UserPermission userPermission)
        {
        }

        #endregion Private Methods
    }
}