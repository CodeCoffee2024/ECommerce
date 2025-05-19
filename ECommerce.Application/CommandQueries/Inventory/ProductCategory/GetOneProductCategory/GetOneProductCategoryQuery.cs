using ECommerce.Application.Abstractions.Messaging;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetOneProductCategory
{
    public sealed record GetOneProductCategoryQuery(Guid Id) : IQuery<GetOneProductCategoryResponse>;
}