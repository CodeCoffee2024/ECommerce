namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators
{
    public interface IAddUnitOfMeasurementTypeCommand
    {
        #region Properties

        Guid Id { get; }
        bool HasDecimal { get; }
        string Name { get; }

        #endregion Properties
    }
}