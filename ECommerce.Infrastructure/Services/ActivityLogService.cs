using ECommerce.Application.Abstractions;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.Log;
using ECommerce.Domain.Entities.Log.Interfaces;

namespace ECommerce.Infrastructure.Services
{
    public class ActivityLogService : IActivityLogService
    {
        #region Fields

        private readonly IDbService _dbService;
        private readonly IActivityLogRepository _activityLogRepository;

        #endregion Fields

        #region Public Constructors

        public ActivityLogService(IDbService dbService, IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
            _dbService = dbService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task LogAsync(string entityName, Guid primaryKey, string eventType, Dictionary<string, string> oldValues, Dictionary<string, string> newValues)
        {
            var log = ActivityLog.Create(
                entityName,
                primaryKey, eventType, oldValues, newValues, DateTime.UtcNow);
            _activityLogRepository.Add(log);
        }

        #endregion Public Methods
    }
}