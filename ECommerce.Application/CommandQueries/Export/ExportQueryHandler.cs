using ECommerce.Application.Abstractions;
using MediatR;

namespace ECommerce.Application.CommandQueries.Export
{
    public class ExportQueryHandler : IRequestHandler<ExportQuery, byte[]>
    {
        #region Fields

        private readonly IExportService _exportService;

        #endregion Fields

        #region Public Constructors

        public ExportQueryHandler(IExportService exportService)
        {
            _exportService = exportService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<byte[]> Handle(ExportQuery request, CancellationToken cancellationToken)
        {
            return await _exportService.ExportAsync(request.Data, request.ExportType, request.ReportName, request.Filters);
        }

        #endregion Public Methods
    }
}