namespace ECommerce.Domain.Dtos.Settings.UnitOfMeasurement
{
    public class UpdateUnitOfMeasurementConversionDTO
    {
        #region Properties

        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public decimal Value { get; set; }

        #endregion Properties
    }
}