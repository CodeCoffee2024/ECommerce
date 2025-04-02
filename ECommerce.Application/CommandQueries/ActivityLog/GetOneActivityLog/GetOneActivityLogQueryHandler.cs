using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Log.Interfaces;

namespace ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog
{
    internal sealed class GetOneActivityLogQueryHandler : IQueryHandler<GetOneActivityLogQuery, GetOneActivityLogResponse>
    {
        #region Fields

        private readonly IActivityLogRepository _activityLogRepository;

        #endregion Fields

        #region Public Constructors

        public GetOneActivityLogQueryHandler(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneActivityLogResponse>> Handle(GetOneActivityLogQuery request, CancellationToken cancellationToken)
        {
            var userPermission = await _activityLogRepository.GetLog(request.PrimaryKey, cancellationToken);

            if (userPermission is null)
            {
                return Result.Failure<GetOneActivityLogResponse>(ValidationErrors.NotFound("Activity Log"));
            }
            return GetOneActivityLogResponse.MapToResponse(userPermission);
        }

        #endregion Public Methods
    }
}