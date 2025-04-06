using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.AddUnitOfMeasurementType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateToDisableUnitOfMeasurementType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateToEnableUnitOfMeasurementType;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.UpdateUnitOfMeasurementType;

namespace ECommerce.Api.Controllers.Settings.UnitOfMeasurementType
{
    public sealed record UnitOfMeasurementTypeRequest
    {
        public string Name { get; init; } = string.Empty;
        public bool HasDecimal { get; init; }
        public AddUnitOfMeasurementTypeCommand SetAddCommand(Guid userId) =>
            new(Name, HasDecimal, userId);
        public UpdateUnitOfMeasurementTypeCommand SetUpdateCommand(Guid Id, Guid userId) =>
            new(Id, Name, HasDecimal, userId);
        public UpdateToDisableUnitOfMeasurementTypeCommand SetToDisableCommand(Guid id, Guid userId) =>
            new(id, userId);
        public UpdateToEnableUnitOfMeasurementTypeCommand SetToEnableCommand(Guid id, Guid userId) =>
            new(id, userId);
    }
}