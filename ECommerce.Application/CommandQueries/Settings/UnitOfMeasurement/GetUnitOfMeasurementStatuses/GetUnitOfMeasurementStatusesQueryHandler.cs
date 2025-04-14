using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurementStatuses
{
    internal sealed class GetUnitOfMeasurementStatusesQueryHandler : IQueryHandler<GetUnitOfMeasurementStatusesQuery, IReadOnlyList<object>>
    {
        #region Public Methods

        public async Task<Result<IReadOnlyList<object>>> Handle(GetUnitOfMeasurementStatusesQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(ECommerce.Domain.Entities.Settings.UnitOfMeasurement.GetStatuses());
        }

        #endregion Public Methods
    }
}