using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.UserManagement
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Public Constructors

        public UserRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Private Methods

        public User FindByUsername(string username)
        {
            var user = DbContext.Users
                .FirstOrDefault(it => it.Username == username || it.Email == username);
            return user!;
        }

        public User? GetUserPermission(Guid userId)
        {
            var user = DbContext.Users
                .Include(u => u.UserUserPermissions)
                    .ThenInclude(uup => uup.UserPermission)
                .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        #endregion Private Methods
    }
}