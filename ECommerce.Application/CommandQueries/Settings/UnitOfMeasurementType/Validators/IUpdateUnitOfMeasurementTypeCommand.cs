namespace ECommerce.Application.CommandQueries.Settings.UnitOfMeasurementType.Validators
{
    public interface IUpdateUnitOfMeasurementTypeCommand
    {
        #region Properties

        Guid Id { get; }
        string Name { get; }
        bool HasDecimal { get; }

        #endregion Properties
    }
}