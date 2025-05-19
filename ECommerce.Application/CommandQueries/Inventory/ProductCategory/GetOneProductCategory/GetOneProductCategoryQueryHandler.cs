using AutoMapper;
using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Commons;
using ECommerce.Domain.Entities.Inventory.Interfaces;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetOneProductCategory
{
    internal sealed class GetOneProductCategoryQueryHandler : IQueryHandler<GetOneProductCategoryQuery, GetOneProductCategoryResponse>
    {
        #region Fields

        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Public Constructors

        public GetOneProductCategoryQueryHandler(IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Result<GetOneProductCategoryResponse>> Handle(GetOneProductCategoryQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (productCategory is null)
            {
                return Result.Failure<GetOneProductCategoryResponse>(ValidationErrors.NotFound("Product Category"));
            }
            return GetOneProductCategoryResponse.MapToResponse(_mapper, productCategory);
        }

        #endregion Public Methods
    }
}