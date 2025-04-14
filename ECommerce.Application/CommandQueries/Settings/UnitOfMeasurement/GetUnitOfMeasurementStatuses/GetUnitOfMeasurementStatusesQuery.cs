using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetUnitOfMeasurementStatuses
{
    public sealed record GetUnitOfMeasurementStatusesQuery() : IQuery<IReadOnlyList<object>>;
}