using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurementDropdown
{
    public class GetUnitOfMeasurementDropdownHandler : IQueryHandler<GetUnitOfMeasurementDropdownQuery, PagedResult<GetUnitOfMeasurementDropdownResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        #endregion Fields

        #region Public Constructors

        public GetUnitOfMeasurementDropdownHandler(IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUnitOfMeasurementDropdownResponse>>> Handle(GetUnitOfMeasurementDropdownQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurement>? result = await _unitOfMeasurementRepository.GetListingPageDropdownResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return result.SetPagedResultResponse(result => GetUnitOfMeasurementDropdownResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}