namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurement.Validators
{
    public interface IAddUnitOfMeasurementCommand
    {
        #region Properties

        Guid Id { get; }
        Guid UnitOfMeasurementTypeId { get; }
        string Name { get; }
        string Abbreviation { get; }

        #endregion Properties
    }
}