using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

namespace ECommerce.Infrastructure.Repositories.UserManagement
{
    internal sealed class UserUserPermissionRepository : IUserUserPermissionRepository
    {
        #region Public Constructors

        private readonly AppDbContext DbContext;

        public UserUserPermissionRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<UserUserPermission> GetUserUserPermissionByUserId(Guid userId)
        {
            var userUserPermissions = DbContext.UserUserPermissions
                .Where(it => it.UserId == userId).AsEnumerable();
            return userUserPermissions!;
        }

        public void Remove(UserUserPermission userUserPermission)
        {
            DbContext.Remove(userUserPermission);
        }

        #endregion Public Methods
    }
}