using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.GetOneUnitOfMeasurement
{
    public sealed record GetOneUnitOfMeasurementQuery(Guid Id) : IQuery<GetOneUnitOfMeasurementResponse>;
}