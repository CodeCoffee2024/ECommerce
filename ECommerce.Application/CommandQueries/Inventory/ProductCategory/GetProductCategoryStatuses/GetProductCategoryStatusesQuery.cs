namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryStatuses
{
    using ECommerce.Application.Abstractions.Messaging;

    public sealed record GetProductCategoryStatusesQuery() : IQuery<IReadOnlyList<object>>;
}