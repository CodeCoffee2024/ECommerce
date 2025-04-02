using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.Log
{
    public class ActivityLog : BaseEntity
    {
        #region Properties

        public string EntityName { get; set; }
        public Guid? PrimaryKey { get; set; }
        public string EventType { get; set; } // Insert, Update, Delete
        public Dictionary<string, string>? OldValues { get; set; } = new();
        public Dictionary<string, string>? NewValues { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        #endregion Properties

        #region Private Constructors

        public ActivityLog()
        {
        }

        private ActivityLog(string entityName, Guid primaryKey, string eventType, Dictionary<string, string>? oldValues, Dictionary<string, string>? newValues, DateTime timeStamp)
        {
            EntityName = entityName;
            PrimaryKey = primaryKey;
            EventType = eventType;
            OldValues = oldValues;
            NewValues = newValues;
        }

        #endregion Private Constructors

        #region Public Methods

        public static ActivityLog Create(string entityName, Guid primaryKey, string eventType, Dictionary<string, string>? oldValues, Dictionary<string, string>? newValues, DateTime timeStamp)
        {
            var activityLog = new ActivityLog(entityName, primaryKey, eventType, oldValues, newValues, timeStamp);
            return activityLog;
        }

        #endregion Public Methods
    }
}