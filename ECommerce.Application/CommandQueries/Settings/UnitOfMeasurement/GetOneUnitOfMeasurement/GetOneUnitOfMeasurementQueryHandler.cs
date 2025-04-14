using AutoMapper;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetOneUnitOfMeasurement
{
    internal sealed class GetOneUnitOfMeasurementQueryHandler : IQueryHandler<GetOneUnitOfMeasurementQuery, GetOneUnitOfMeasurementResponse>
    {
        #region Fields

        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Public Constructors

        public GetOneUnitOfMeasurementQueryHandler(IMapper mapper, IUnitOfMeasurementRepository unitOfMeasurementRepository)
        {
            _mapper = mapper;
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneUnitOfMeasurementResponse>> Handle(GetOneUnitOfMeasurementQuery request, CancellationToken cancellationToken)
        {
            var unitOfMeasurement = await _unitOfMeasurementRepository.GetByIdAsync(request.Id, cancellationToken);

            if (unitOfMeasurement is null)
            {
                return Result.Failure<GetOneUnitOfMeasurementResponse>(ValidationErrors.NotFound("Unit of Measurement"));
            }
            return GetOneUnitOfMeasurementResponse.MapToResponse(_mapper, unitOfMeasurement);
        }

        #endregion Public Methods
    }
}