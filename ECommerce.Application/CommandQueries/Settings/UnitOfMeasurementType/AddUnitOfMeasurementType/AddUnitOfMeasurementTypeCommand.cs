using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.AddUnitOfMeasurementType
{
    public sealed record AddUnitOfMeasurementTypeCommand(
        string Name,
        bool HasDecimal,
        Guid Id) : ICommand, IAddUnitOfMeasurementTypeCommand;
}