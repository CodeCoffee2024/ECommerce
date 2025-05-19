namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryDropdown
{
    public sealed class GetProductCategoryDropdownResponse
    {
        #region Properties

        public string Name { get; set; } = string.Empty;
        public Guid? Id { get; set; }

        #endregion Properties

        #region Internal Methods

        internal static GetProductCategoryDropdownResponse MapToResponse(ECommerce.Domain.Entities.Inventory.ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));

            return new GetProductCategoryDropdownResponse()
            {
                Name = productCategory.Name,
                Id = productCategory.Id,
            };
        }

        #endregion Internal Methods
    }
}