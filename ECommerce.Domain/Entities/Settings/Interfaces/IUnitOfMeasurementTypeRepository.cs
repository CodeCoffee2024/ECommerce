using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Domain.Entities.Settings.Interfaces
{
    public interface IUnitOfMeasurementTypeRepository
    {
        #region Public Methods

        Task<PagedResult<UnitOfMeasurementType>> GetListingPageResultAsync(UnitOfMeasurementTypeDTO searchValue, CancellationToken cancellationToken);

        Task<PagedResult<UnitOfMeasurementType>> GetListingPageDropdownResultAsync(DefaultFilterBaseDto searchValue, CancellationToken cancellationToken);

        UnitOfMeasurementType FindByName(string name);

        Task<UnitOfMeasurementType?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<UnpagedResult<UnitOfMeasurementType>> GetListingPageResultExportAsync(UnitOfMeasurementTypeDTO searchValue, CancellationToken cancellationToken);

        void Add(UnitOfMeasurementType unitOfMeasurementType);

        void Remove(UnitOfMeasurementType unitOfMeasurementType);

        void Update(UnitOfMeasurementType unitOfMeasurementType);

        #endregion Public Methods
    }
}