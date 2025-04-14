using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.ExportUnitOfMeasurementListing
{
    public class ExportUnitOfMeasurementQueryHandler : IQueryHandler<ExportUnitOfMeasurementQuery, UnpagedResult<ExportUnitOfMeasurementResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public ExportUnitOfMeasurementQueryHandler(IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<UnpagedResult<ExportUnitOfMeasurementResponse>>> Handle(ExportUnitOfMeasurementQuery request, CancellationToken cancellationToken)
        {
            UnpagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurement>? user = await _unitOfMeasurementRepository.GetListingPageResultExportAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);

            return user.SetResultResponse(result => ExportUnitOfMeasurementResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}