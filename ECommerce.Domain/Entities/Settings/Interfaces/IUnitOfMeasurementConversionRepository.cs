//using ECommerce.Domain.Dtos.Settings.UnitOfMeasurementConversion;

namespace ECommerce.Domain.Entities.Settings.Interfaces
{
    public interface IUnitOfMeasurementConversionRepository
    {
        #region Public Methods

        void Add(UnitOfMeasurementConversion unitOfMeasurementConversion);

        void Remove(UnitOfMeasurementConversion unitOfMeasurementConversion);

        void Update(UnitOfMeasurementConversion unitOfMeasurementConversion);

        #endregion Public Methods
    }
}