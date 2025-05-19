using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateProductCategory
{
    public sealed record UpdateProductCategoryCommand(
        Guid Id,
        Guid? ParentProductCategoryId,
        bool IsSubCategory,
        string Name,
        Guid UserId) : ICommand, IUpdateProductCategoryCommand;
}