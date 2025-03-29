namespace ECommerce.Domain.Entities.Log
{
    public class Log
    {
        #region Properties

        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        #endregion Properties
    }
}