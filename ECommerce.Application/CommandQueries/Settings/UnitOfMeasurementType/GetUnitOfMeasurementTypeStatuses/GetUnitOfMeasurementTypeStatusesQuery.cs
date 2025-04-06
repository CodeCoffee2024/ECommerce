using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetUnitOfMeasurementTypeStatuses
{
    public sealed record GetUnitOfMeasurementTypeStatusesQuery() : IQuery<IReadOnlyList<object>>;
}