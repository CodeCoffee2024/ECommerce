namespace ECommerce.Application.CommandQueries.ActivityLog.GetOneActivityLog
{
    public class GetOneActivityLogResponse
    {
        #region Properties

        public Guid? Id { get; set; }
        public Guid? PrimaryKey { get; set; }
        public string EventType { get; set; } = string.Empty;
        public Dictionary<string, string>? OldValues { get; set; }
        public Dictionary<string, string>? NewValues { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion Properties

        #region Internal Methods

        internal static GetOneActivityLogResponse MapToResponse(ECommerce.Domain.Entities.Log.ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            return new GetOneActivityLogResponse()
            {
                Id = activityLog.Id,
                PrimaryKey = activityLog.PrimaryKey,
                EventType = activityLog.EventType,
                OldValues = activityLog.OldValues,
                NewValues = activityLog.NewValues,
                TimeStamp = activityLog.Timestamp
            };
        }

        #endregion Internal Methods
    }
}