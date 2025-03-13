using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;

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
                .FirstOrDefault(it => it.Username == username);
            return user!;
        }

        #endregion Private Methods
    }
}