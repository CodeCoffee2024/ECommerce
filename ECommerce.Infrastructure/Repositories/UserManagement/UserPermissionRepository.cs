using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Entities.UserManagement;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public UserPermission FindByName(string name)
        {
            var userPermission = DbContext.UserPermissions
                .FirstOrDefault(it => it.Name.ToLower() == name.ToLower());
            return userPermission!;
        }

        public async Task<IEnumerable<UserPermission>> GetAllPermissions(CancellationToken cancellationToken)
        {
            var result = DbContext.UserPermissions.AsEnumerable();
            return result;
        }

        public async Task<PagedResult<UserPermission>> GetListingPageResultAsync(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UserPermissions.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(userPermission =>
                  userPermission.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  userPermission.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  userPermission.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
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

        public async Task<PagedResult<UserPermission>> GetListingPageDropdownResultAsync(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UserPermissions.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(userPermission =>
                    userPermission.Name.Contains(listFilterDto.GlobalSearchValue)
                );
                if (listFilterDto.Exclude != "")
                {
                    query = query.Where(userPermission =>
                        userPermission.Name.Contains(listFilterDto.GlobalSearchValue) && !listFilterDto.Exclude!.Contains(userPermission.Name)
                    );
                }
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

        public async Task<UnpagedResult<UserPermission>> GetListingPageResultExportAsync(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.UserPermissions.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();

            if (listFilterDto.HasSearchValues)
            {
                query = query.Where(userPermission =>
                  userPermission.Name.Contains(listFilterDto.GlobalSearchValue) ||
                  userPermission.CreatedBy.FirstName.Contains(listFilterDto.GlobalSearchValue) ||
                  userPermission.CreatedBy.MiddleName.Contains(listFilterDto.GlobalSearchValue));
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

        public async Task<List<UserPermission>> GetListingResultAsync(
            string search, int? page, string sortBy, string sortDirection, string createdBy, CancellationToken cancellationToken = default)
        {
            const int pageSize = 10; // Default page size

            var query = DbContext.Set<UserPermission>().AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(up => up.Permissions.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(createdBy))
            {
                query = query.Where(up => up.CreatedBy.LastName.Contains(createdBy) || up.CreatedBy.FirstName.Contains(createdBy));
            }

            // Sorting
            query = sortDirection.ToLower() switch
            {
                "desc" => query.OrderByDescending(up => EF.Property<object>(up, sortBy)),
                _ => query.OrderBy(up => EF.Property<object>(up, sortBy))
            };

            // Pagination
            var result = await query
                .Skip((page.Value - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }

        #endregion Private Methods
    }
}