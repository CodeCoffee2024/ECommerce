using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateToDisableUnitOfMeasurement
{
    public sealed record UpdateToDisableUnitOfMeasurementCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableUnitOfMeasurementTypeCommand;
}