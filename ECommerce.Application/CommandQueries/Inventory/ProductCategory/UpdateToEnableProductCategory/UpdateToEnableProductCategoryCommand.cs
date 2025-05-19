using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateToEnableProductCategory
{
    public sealed record UpdateToEnableProductCategoryCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableProductCategoryCommand;
}