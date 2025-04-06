using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Settings.UnitOfMeasurement;

namespace ECommerce.Domain.Entities.Settings.Interfaces
{
    public interface IUnitOfMeasurementRepository
    {
        #region Public Methods

        Task<PagedResult<UnitOfMeasurement>> GetListingPageResultAsync(UnitOfMeasurementDTO searchValue, CancellationToken cancellationToken);

        #endregion Public Methods
    }
}