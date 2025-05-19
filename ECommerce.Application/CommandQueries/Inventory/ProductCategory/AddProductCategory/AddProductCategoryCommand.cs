using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.AddProductCategory
{
    public sealed record AddProductCategoryCommand(
        string Name,
        Guid? ParentProductCategoryId,
        bool? IsSubCategory,
        Guid Id) : ICommand, IAddProductCategoryCommand;
}