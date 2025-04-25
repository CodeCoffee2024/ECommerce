using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateToEnableUnitOfMeasurement
{
    public sealed record UpdateToEnableUnitOfMeasurementCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableUnitOfMeasurementTypeCommand;
}