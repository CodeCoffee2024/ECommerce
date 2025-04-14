using AutoMapper;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetOneUnitOfMeasurentType
{
    internal sealed class GetOneUnitOfMeasurementTypeQueryHandler : IQueryHandler<GetOneUnitOfMeasurementTypeQuery, GetOneUnitOfMeasurementTypeResponse>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Public Constructors

        public GetOneUnitOfMeasurementTypeQueryHandler(IMapper mapper, IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _mapper = mapper;
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneUnitOfMeasurementTypeResponse>> Handle(GetOneUnitOfMeasurementTypeQuery request, CancellationToken cancellationToken)
        {
            var unitOfMeasurement = await _unitOfMeasurementTypeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (unitOfMeasurement is null)
            {
                return Result.Failure<GetOneUnitOfMeasurementTypeResponse>(ValidationErrors.NotFound("User Permission"));
            }
            return GetOneUnitOfMeasurementTypeResponse.MapToResponse(_mapper, unitOfMeasurement);
        }

        #endregion Public Methods
    }
}