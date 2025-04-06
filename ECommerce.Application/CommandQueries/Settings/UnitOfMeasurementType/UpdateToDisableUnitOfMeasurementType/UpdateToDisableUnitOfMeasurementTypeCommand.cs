using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateToDisableUnitOfMeasurementType
{
    public sealed record UpdateToDisableUnitOfMeasurementTypeCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableUnitOfMeasurementTypeCommand;
}