using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateToEnableUnitOfMeasurementType
{
    public sealed record UpdateToEnableUnitOfMeasurementTypeCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableUnitOfMeasurementTypeCommand;
}