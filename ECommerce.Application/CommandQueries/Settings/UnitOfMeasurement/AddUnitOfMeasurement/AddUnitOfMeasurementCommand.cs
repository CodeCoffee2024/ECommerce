using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.AddUnitOfMeasurement
{
    public sealed record AddUnitOfMeasurementCommand(
        string Name,
        string Abbreviation,
        Guid UnitOfMeasurementTypeId,
        Guid Id) : ICommand, IAddUnitOfMeasurementCommand;
}