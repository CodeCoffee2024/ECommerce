using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateUnitOfMeasurementType
{
    public sealed record UpdateUnitOfMeasurementTypeCommand(
        Guid Id,
        string Name,
        bool HasDecimal,
        Guid UserId) : ICommand, IUpdateUnitOfMeasurementTypeCommand;
}