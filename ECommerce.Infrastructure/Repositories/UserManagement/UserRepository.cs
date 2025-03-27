using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.UserManagement.User;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Infrastructure.Data;
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
                .FirstOrDefault(it => it.Username == username);
            return user!;
        }

        public User FindByEmail(string email)
        {
            var user = DbContext.Users
                .FirstOrDefault(it => it.Email == email);
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

        public async Task<PagedResult<User>> GetListingPageResultAsync(UserListingDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.Users.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(user =>
                  user.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  user.LastName.Contains(listFilterDto.GlobalSearchValue) ||
                  (user.MiddleName.Contains(listFilterDto.GlobalSearchValue) && user.MiddleName != string.Empty) ||
                  user.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  user.UserUserPermissions.Any(it => it.UserPermission.Name.Contains(listFilterDto.GlobalSearchValue)) ||
                  user.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }
            if (listFilterDto.UserPermissions != "" && listFilterDto.UserPermissions != null)
            {
                query = query.Where(user =>
                    user.UserUserPermissions.Any(it => listFilterDto.UserPermissions.Contains(it.UserPermission.Name))
                );
            }

            var list = await query
                .OrderByDescending(r => r.ModifiedDate)
                .AsNoTracking()
                .PaginateAsync(
                    listFilterDto.Page,
                    listFilterDto.PageSize,
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        public async Task<UnpagedResult<User>> GetListingPageResultExportAsync(UserListingDTO listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.Users.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(user =>
                  user.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  user.LastName.Contains(listFilterDto.GlobalSearchValue) ||
                  (user.MiddleName.Contains(listFilterDto.GlobalSearchValue) && user.MiddleName != string.Empty) ||
                  user.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  user.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
            }

            if (listFilterDto.UserPermissions != "" && listFilterDto.UserPermissions != null)
            {
                query = query.Where(user =>
                    user.UserUserPermissions.Any(it => listFilterDto.UserPermissions.Contains(it.UserPermission.Name))
                );
            }
            var list = await query
                .OrderByDescending(r => r.ModifiedDate)
                .AsNoTracking()
                .UnpaginateAsync(
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        #endregion Private Methods
    }
}