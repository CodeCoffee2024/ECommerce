using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.Validators
{
    public interface IUpdateUnitOfMeasurementCommand
    {
        #region Properties

        Guid Id { get; }
        List<UpdateUnitOfMeasurementConversionDTO> Conversions { get; }
        string Name { get; }
        string Abbreviation { get; }

        #endregion Properties
    }
}