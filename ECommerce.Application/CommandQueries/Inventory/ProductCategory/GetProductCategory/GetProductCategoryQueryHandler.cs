using AutoMapper;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Inventory.Interfaces;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory
{
    public class GetProductCategoryQueryHandler : IQueryHandler<GetProductCategoryQuery, PagedResult<GetProductCategoryResponse>>
    {
        #region Fields

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        // Simulated user storage (replace with DB access in production)

        #region Public Constructors

        public GetProductCategoryQueryHandler(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<PagedResult<GetProductCategoryResponse>>> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
        {
            PagedResult<ECommerce.Domain.Entities.Inventory.ProductCategory>? productCategory = await _productCategoryRepository.GetListingPageResultAsync(request.SetGlobalSearchValueFilterDTO(), cancellationToken);
            return productCategory.SetPagedResultResponse(result => GetProductCategoryResponse.MapToResponse(_mapper, result));
        }

        #endregion Public Methods
    }
}