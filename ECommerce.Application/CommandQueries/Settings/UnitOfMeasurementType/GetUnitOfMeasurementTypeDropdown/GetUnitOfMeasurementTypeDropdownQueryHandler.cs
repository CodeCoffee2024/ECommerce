using ECommerce.Api.Controllers.Settings.UnitOfMeasurementType;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementDropdown
{
    public class GetUnitOfMeasurementTypeDropdownQueryHandler : IQueryHandler<GetUnitOfMeasurementTypeDropdownQuery, PagedResult<GetUnitOfMeasurementTypeDropdownResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUnitOfMeasurementTypeDropdownQueryHandler(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUnitOfMeasurementTypeDropdownResponse>>> Handle(GetUnitOfMeasurementTypeDropdownQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurementType>? result = await _unitOfMeasurementTypeRepository.GetListingPageDropdownResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return result.SetPagedResultResponse(result => GetUnitOfMeasurementTypeDropdownResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}