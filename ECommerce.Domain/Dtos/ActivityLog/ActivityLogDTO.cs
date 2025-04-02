namespace ECommerce.Domain.Dtos.ActivityLog
{
    public class ActivityLogDTO
    {
        #region Properties

        public ActivityLogDTO(Guid id, Guid primaryKey, string eventType, Dictionary<string, string> oldValues, Dictionary<string, string> newValues, DateTime timeStamp)
        {
            Id = Id;
            PrimaryKey = primaryKey;
            EventType = eventType;
            OldValues = oldValues;
            NewValues = newValues;
        }

        public Guid Id { get; set; }
        public Guid PrimaryKey { get; set; }
        public string EventType { get; set; } = string.Empty;
        public Dictionary<string, string> OldValues { get; set; }
        public Dictionary<string, string> NewValues { get; set; }

        #endregion Properties
    }
}