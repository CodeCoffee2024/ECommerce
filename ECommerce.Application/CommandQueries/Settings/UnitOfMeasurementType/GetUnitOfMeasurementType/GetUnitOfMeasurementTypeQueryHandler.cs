﻿using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Settings.Interfaces;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementType
{
    public class GetUnitOfMeasurementTypeQueryHandler : IQueryHandler<GetUnitOfMeasurementTypeQuery, PagedResult<GetUnitOfMeasurementTypeResponse>>
    {
        #region Fields

        private readonly IUnitOfMeasurementTypeRepository _unitOfMeasurementTypeRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetUnitOfMeasurementTypeQueryHandler(IUnitOfMeasurementTypeRepository unitOfMeasurementTypeRepository)
        {
            _unitOfMeasurementTypeRepository = unitOfMeasurementTypeRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetUnitOfMeasurementTypeResponse>>> Handle(GetUnitOfMeasurementTypeQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Settings.UnitOfMeasurementType>? unitOfMeasurementType = await _unitOfMeasurementTypeRepository.GetListingPageResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return unitOfMeasurementType.SetPagedResultResponse(result => GetUnitOfMeasurementTypeResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}