namespace ECommerce.Application.Abstractions
{
    public interface IActivityLogService
    {
        #region Public Methods

        Task LogAsync(string entityName, Guid primaryKey, string eventType, Dictionary<string, string> oldValues, Dictionary<string, string> newValues);

        #endregion Public Methods
    }
}