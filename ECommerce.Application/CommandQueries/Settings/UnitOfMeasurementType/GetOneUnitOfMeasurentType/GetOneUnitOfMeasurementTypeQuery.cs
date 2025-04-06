using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.GetOneUnitOfMeasurentType
{
    public sealed record GetOneUnitOfMeasurementTypeQuery(Guid Id) : IQuery<GetOneUnitOfMeasurementTypeResponse>;
}