using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.Validators;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.UpdateToDisableProductCategory
{
    public sealed record UpdateToDisableProductCategoryCommand(
        Guid Id,
        Guid UserId) : ICommand, IUpdateToEnableDisableProductCategoryCommand;
}