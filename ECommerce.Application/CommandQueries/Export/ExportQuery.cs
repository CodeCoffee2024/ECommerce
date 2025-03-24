using MediatR;

namespace ECommerce.Application.CommandQueries.Export
{
    public sealed record ExportQuery(List<object> Data, string ExportType, string ReportName, Dictionary<string, string> Filters) : IRequest<byte[]>;
}