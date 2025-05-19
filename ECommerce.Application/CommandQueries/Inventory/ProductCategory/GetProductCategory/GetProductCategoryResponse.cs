using AutoMapper;
using ECommerce.Application.CommandQueries.Common.Mapping;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory
{
    public sealed class GetProductCategoryResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserFragmentResponse CreatedBy { get; set; } = new();
        public UserFragmentResponse ModifiedBy { get; set; } = new();

        #endregion Properties

        #region Methods

        internal static GetProductCategoryResponse MapToResponse(IMapper mapper, ECommerce.Domain.Entities.Inventory.ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));

            return new GetProductCategoryResponse()
            {
                Name = productCategory.Name,
                Id = productCategory.Id,
                CreatedDate = productCategory.CreatedDate,
                ModifiedDate = productCategory.ModifiedDate,
                Status = productCategory.Status,
                CreatedBy = mapper.Map<UserFragmentResponse>(productCategory.CreatedBy),
                ModifiedBy = mapper.Map<UserFragmentResponse>(productCategory.ModifiedBy)
            };
        }

        #endregion Methods
    }
}