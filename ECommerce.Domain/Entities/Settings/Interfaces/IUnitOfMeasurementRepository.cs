using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Domain.Entities.Settings.Interfaces
{
    public interface IUnitOfMeasurementRepository
    {
        #region Public Methods

        Task<PagedResult<UnitOfMeasurement>> GetListingPageResultAsync(UnitOfMeasurementDTO searchValue, CancellationToken cancellationToken);

        Task<PagedResult<UnitOfMeasurement>> GetListingPageDropdownResultAsync(DefaultFilterBaseDto searchValue, CancellationToken cancellationToken);

        UnitOfMeasurement FindByName(string name);

        UnitOfMeasurement FindByAbbreviation(string abbreviation);

        Task<UnitOfMeasurement?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<UnpagedResult<UnitOfMeasurement>> GetListingPageResultExportAsync(UnitOfMeasurementDTO searchValue, CancellationToken cancellationToken);

        void Add(UnitOfMeasurement unitOfMeasurement);

        void Remove(UnitOfMeasurement unitOfMeasurement);

        void Update(UnitOfMeasurement unitOfMeasurement);

        #endregion Public Methods
    }
}