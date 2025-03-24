using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ECommerce.Infrastructure.Data
{
    internal static class UnpaginateQueryableBehavior
    {
        #region Public Methods

        public static async Task<Domain.Commons.UnpagedResult<T>> UnpaginateAsync<T>(
            this IQueryable<T> query,
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
              .ToListAsync();

            return new Domain.Commons.UnpagedResult<T>(entities, totalRecords, totalEntries);
        }

        public static Domain.Commons.UnpagedResult<T> Unpaginate<T>(
            this List<T> query,
            int totalEntries = 0)
        {
            var totalRecords = query.Count();

            var entities = query
                .ToList();

            return new Domain.Commons.UnpagedResult<T>(entities, totalRecords, totalEntries);
        }

        #endregion Public Methods
    }
}