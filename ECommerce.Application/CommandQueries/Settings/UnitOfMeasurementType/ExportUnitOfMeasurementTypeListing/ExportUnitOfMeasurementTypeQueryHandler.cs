using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.ExportUnitOfMeasurementTypeListing
{
    public class ExportUnitOfMeasurementTypeQueryHandler : IQueryHandler<ExportUnitOfMeasurementTypeQuery, UnpagedResult<ExportUnitOfMeasurementTypeResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public ExportUnitOfMeasurementTypeQueryHandler(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<UnpagedResult<ExportUnitOfMeasurementTypeResponse>>> Handle(ExportUnitOfMeasurementTypeQuery request, CancellationToken cancellationToken)
        {
            UnpagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurementType>? user = await _unitOfMeasurementTypeRepository.GetListingPageResultExportAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);

            return user.SetResultResponse(result => ExportUnitOfMeasurementTypeResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}