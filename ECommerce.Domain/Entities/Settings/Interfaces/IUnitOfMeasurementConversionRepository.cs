//using ECommerce.Domain.Dtos.Settings.UnitOfMeasurementConversion;

namespace ECommerce.Domain.Entities.Settings.Interfaces
{
    public interface IUnitOfMeasurementConversionRepository
    {
        #region Public Methods

        Task<UnitOfMeasurementConversion?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<UnitOfMeasurementConversion?> GetCounterpartById(Guid FromId, Guid ToId, CancellationToken cancellationToken = default);

        void Add(UnitOfMeasurementConversion unitOfMeasurementConversion);

        void Remove(UnitOfMeasurementConversion unitOfMeasurementConversion);

        void Update(UnitOfMeasurementConversion unitOfMeasurementConversion);

        #endregion Public Methods
    }
}