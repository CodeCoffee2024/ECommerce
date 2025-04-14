using AutoMapper;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurement
{
    public class GetUnitOfMeasurementQueryHandler : IQueryHandler<GetUnitOfMeasurementQuery, PagedResult<GetUnitOfMeasurementResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUnitOfMeasurementQueryHandler(IMapper mapper, IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _mapper = mapper;
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUnitOfMeasurementResponse>>> Handle(GetUnitOfMeasurementQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurement>? unitOfMeasurement = await _unitOfMeasurementRepository.GetListingPageResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return unitOfMeasurement.SetPagedResultResponse(result => GetUnitOfMeasurementResponse.MapToResponse(_mapper, result));
        }

        #endregion Public Methods
    }
}