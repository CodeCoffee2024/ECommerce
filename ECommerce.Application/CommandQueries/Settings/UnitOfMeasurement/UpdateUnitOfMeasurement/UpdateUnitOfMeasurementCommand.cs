using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.Validators;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.UpdateUnitOfMeasurement
{
    public sealed record UpdateUnitOfMeasurementCommand(
        Guid Id,
        string Name,
        string Abbreviation,
        List<UpdateUnitOfMeasurementConversionDTO> Conversions,
        Guid UserId) : ICommand, IUpdateUnitOfMeasurementCommand;
}