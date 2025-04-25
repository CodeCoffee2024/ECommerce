namespace ECommerce.Domain.Dtos.Settings.UnitOfMeasurement
{
    public class UpdateUnitOfMeasurementConversionDTO
    {
        #region Properties

        public Guid? Id { get; set; }
        public Guid? UnitOfMeasurementFrom { get; set; }
        public Guid? UnitOfMeasurementTo { get; set; }
        public decimal Value { get; set; }

        #endregion Properties
    }
}