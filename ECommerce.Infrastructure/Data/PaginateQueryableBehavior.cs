using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    internal static class PaginateQueryableBehavior
    {
        #region Public Methods

        public static async Task<Domain.Commons.PagedResult<T>> PaginateAsync<T>(
            this IQueryable<T> query,
            int pageIndex = 1,
            int pageSize = 10,
            string? sortKey = "",
            string? sortDirection = "desc",
            int totalEntries = 0)
        {
            // Apply sorting if sortKey has a value
            if (!string.IsNullOrWhiteSpace(sortKey) && !string.IsNullOrEmpty(sortDirection))
            {
                var sortExpression = $"{sortKey} {sortDirection}";
                query = query.OrderBy(sortExpression);
            }

            var totalRecords = await query.CountAsync();

            var entities = await query
              .Skip((pageIndex - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync();

            return new Domain.Commons.PagedResult<T>(entities, pageIndex, pageSize, totalRecords, totalEntries);
        }

        public static Domain.Commons.PagedResult<T> Paginate<T>(
            this List<T> query,
            int pageIndex = 1,
            int pageSize = 10,
            int totalEntries = 0)
        {
            var totalRecords = query.Count();

            var entities = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Domain.Commons.PagedResult<T>(entities, pageIndex, pageSize, totalRecords, totalEntries);
        }

        #endregion Public Methods
    }
}