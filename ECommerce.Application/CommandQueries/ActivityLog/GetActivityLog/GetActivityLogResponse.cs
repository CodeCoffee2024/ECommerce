namespace ECommerce.Domain.Dtos.ActivityLog.GetActivityLog
{
    public class GetActivityLogResponse
    {
        #region Properties

        public Guid? Id { get; set; }
        public Guid? PrimaryKey { get; set; }
        public string EventType { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }

        internal static GetActivityLogResponse MapToResponse(ECommerce.Domain.Entities.Log.ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            return new GetActivityLogResponse()
            {
                Id = activityLog.Id,
                PrimaryKey = activityLog.PrimaryKey,
                EventType = activityLog.EventType,
                TimeStamp = activityLog.Timestamp
            };
        }

        #endregion Properties
    }
}