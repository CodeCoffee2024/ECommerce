using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Inventory.ProductCategory;

namespace ECommerce.Domain.Entities.Inventory.Interfaces
{
    public interface IProductCategoryRepository
    {
        #region Public Methods

        Task<PagedResult<ProductCategory>> GetListingPageResultAsync(ProductCategoryDTO searchValue, CancellationToken cancellationToken);

        Task<PagedResult<ProductCategory>> GetListingPageDropdownResultAsync(ProductCategoryDTO searchValue, CancellationToken cancellationToken);

        ProductCategory FindByName(string name);

        Task<ProductCategory?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<UnpagedResult<ProductCategory>> GetListingPageResultExportAsync(ProductCategoryDTO searchValue, CancellationToken cancellationToken);

        void Add(ProductCategory unitOfMeasurementType);

        void Remove(ProductCategory unitOfMeasurementType);

        void Update(ProductCategory unitOfMeasurementType);

        #endregion Public Methods
    }
}