using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Domain.Entities.Log.Interfaces
{
    public interface IActivityLogRepository
    {
        #region Public Methods

        void Add(ActivityLog log);

        Task<PagedResult<ActivityLog>> GetAllLogs(DefaultFilterBaseDto listFilterDto, CancellationToken cancellationToken);

        Task<ActivityLog>? GetLog(Guid PrimaryKey, CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}