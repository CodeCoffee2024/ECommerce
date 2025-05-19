using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetOneProductCategory
{
    public sealed class GetOneProductCategoryResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool? IsSubCategory { get; set; }
        public ProductCategoryFragmentResponse? ParentProductCategory { get; set; }
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetOneProductCategoryResponse MapToResponse(IMapper mapper, ECommerce.Domain.Entities.Inventory.ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));

            return new GetOneProductCategoryResponse()
            {
                Name = productCategory.Name,
                Id = productCategory.Id,
                CreatedDate = productCategory.CreatedDate,
                IsSubCategory = productCategory.IsSubCategory,
                ModifiedDate = productCategory.ModifiedDate,
                Status = productCategory.Status,
                ParentProductCategory = mapper.Map<ProductCategoryFragmentResponse>(productCategory.ParentProductCategory),
                CreatedBy = mapper.Map<UserFragmentResponse>(productCategory.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(productCategory.ModifiedBy)
            };
        }

        #endregion Methods
    }
}