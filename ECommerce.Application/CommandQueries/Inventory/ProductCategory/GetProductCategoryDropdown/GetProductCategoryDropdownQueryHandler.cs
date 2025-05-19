using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Inventory.Interfaces;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryDropdown
{
    public class GetProductCategoryDropdownQueryHandler : IQueryHandler<GetProductCategoryDropdownQuery, PagedResult<GetProductCategoryDropdownResponse>>
    {
        #region Fields

        private readonly IProductCategoryRepository _productCategoryRepository;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetProductCategoryDropdownQueryHandler(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetProductCategoryDropdownResponse>>> Handle(GetProductCategoryDropdownQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Inventory.ProductCategory>? result = await _productCategoryRepository.GetListingPageDropdownResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return result.SetPagedResultResponse(result => GetProductCategoryDropdownResponse.MapToResponse(result));
        }

        #endregion Public Methods
    }
}