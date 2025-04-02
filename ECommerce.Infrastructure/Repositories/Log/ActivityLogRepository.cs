using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Entities.Log;
using ECommerce.Domain.Entities.Log.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Log
{
    internal sealed class ActivityLogRepository : RepositoryBase<ActivityLog>, IActivityLogRepository
    {
        #region Public Constructors

        public ActivityLogRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<PagedResult<ActivityLog>> GetAllLogs(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken)
        {
            var query = DbContext.ActivityLogs.AsQueryable();
            var queryCount = await query.AsNoTracking().CountAsync();
            query = query.Where(user =>
              user.PrimaryKey == listFilterDto.Id);

            var list = await query
                .OrderByDescending(r => r.Timestamp)
                .AsNoTracking()
                .PaginateAsync(
                    listFilterDto.Page,
                    listFilterDto.PageSize,
                    listFilterDto.SortBy,
                    listFilterDto.SortDirection,
                    queryCount);

            return list;
        }

        public async Task<ActivityLog> GetLog(Guid id, CancellationToken cancellationToken)
        {
            var activityLog = await DbContext.ActivityLogs
                .FirstOrDefaultAsync(it => it.Id == id);
            return activityLog!;
        }

        #endregion Public Constructors
    }
}