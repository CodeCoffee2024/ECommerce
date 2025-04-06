using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementTypeStatuses
{
    internal sealed class GetUnitOfMeasurementTypeStatusesQueryHandler : IQueryHandler<GetUnitOfMeasurementTypeStatusesQuery, IReadOnlyList<object>>
    {
        #region Public Methods

        public async Task<Result<IReadOnlyList<object>>> Handle(GetUnitOfMeasurementTypeStatusesQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(ECommerce.Domain.Entities.Settings.UnitOfMeasurementType.GetStatuses());
        }

        #endregion Public Methods
    }
}