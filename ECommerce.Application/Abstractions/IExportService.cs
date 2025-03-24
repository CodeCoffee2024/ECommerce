namespace ECommerce.Application.Abstractions
{
    public interface IExportService
    {
        #region Public Methods

        Task<byte[]> ExportAsync(List<object> data, string exportType, string reportName, Dictionary<string, string> filters);

        #endregion Public Methods
    }
}