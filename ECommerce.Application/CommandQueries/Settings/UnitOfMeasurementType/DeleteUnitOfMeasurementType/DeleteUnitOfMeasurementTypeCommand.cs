using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.DeleteUnitOfMeasurementType
{
    public sealed record DeleteUnitOfMeasurementTypeCommand(Guid Id) : ICommand;
}