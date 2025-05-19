using ECommerce.Application.CommandQueries.Inventory.ProductCategory.AddProductCategory;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateProductCategory;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateToDisableProductCategory;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateToEnableProductCategory;

namespace ECommerce.Api.Controllers.Inventory.ProductCategory
{
    public sealed record ProductCategoryRequest
    {
        public string Name { get; init; } = string.Empty;
        public bool IsSubCategory { get; init; }
        public Guid? ParentProductCategoryId { get; init; }
        public AddProductCategoryCommand SetAddCommand(Guid userId) =>
            new(Name, ParentProductCategoryId, IsSubCategory, userId);
        public UpdateProductCategoryCommand SetUpdateCommand(Guid Id, Guid userId) =>
            new(Id, ParentProductCategoryId, IsSubCategory, Name, userId);
        public UpdateToDisableProductCategoryCommand SetToDisableCommand(Guid id, Guid userId) =>
            new(id, userId);
        public UpdateToEnableProductCategoryCommand SetToEnableCommand(Guid id, Guid userId) =>
            new(id, userId);
    }
}