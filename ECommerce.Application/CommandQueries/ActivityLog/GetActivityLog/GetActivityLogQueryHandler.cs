using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.ActivityLog.GetActivityLog;
using ECommerce.Domain.Entities.Log.Interfaces;

namespace ECommerce.Application.CommandQueries.ActivityLog.GetActivityLog
{
    public class GetActivityLogQueryHandler : IQueryHandler<GetActivityLogQuery, PagedResult<GetActivityLogResponse>>
    {
        #region Fields

        private readonly IActivityLogRepository _activityLogRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetActivityLogQueryHandler(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetActivityLogResponse>>> Handle(GetActivityLogQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Log.ActivityLog>? activityLog = await _activityLogRepository.GetAllLogs(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return activityLog.SetPagedResultResponse(result => GetActivityLogResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}