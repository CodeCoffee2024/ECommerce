using ECommerce.Application.CommandQueries.Export;

namespace ECommerce.Application.CommandQueries.Common
{
    public sealed record ExportRequest
    {
        #region Properties

        public List<object> Data { get; set; }
        public string ExportType { get; set; }
        public string ReportName { get; set; }
        public Dictionary<string, string> Filters { get; set; }

        public ExportQuery Export() =>
            new ExportQuery(Data, ExportType, ReportName, Filters);

        #endregion Properties
    }
}