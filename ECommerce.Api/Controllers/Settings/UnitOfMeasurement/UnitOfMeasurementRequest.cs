﻿using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.AddUnitOfMeasurement;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateToDisableUnitOfMeasurement;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateToEnableUnitOfMeasurement;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateUnitOfMeasurement;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Api.Controllers.Settings.UnitOfMeasurement
{
    public sealed record UnitOfMeasurementRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Abbreviation { get; init; } = string.Empty;
        public List<UpdateUnitOfMeasurementConversionDTO>? Conversions { get; init; }
        public Guid UnitOfMeasurementTypeId { get; init; }
        public AddUnitOfMeasurementCommand SetAddCommand(Guid userId) =>
            new(Name, Abbreviation, UnitOfMeasurementTypeId, userId);
        public UpdateUnitOfMeasurementCommand SetUpdateCommand(Guid Id, Guid userId) =>
            new(Id, Name, Abbreviation, Conversions!, userId);
        public UpdateToDisableUnitOfMeasurementCommand SetToDisableCommand(Guid id, Guid userId) =>
            new(id, userId);
        public UpdateToEnableUnitOfMeasurementCommand SetToEnableCommand(Guid id, Guid userId) =>
            new(id, userId);
    }
}