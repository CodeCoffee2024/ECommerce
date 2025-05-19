using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory
{
    public class GetProductCategoryQuery : GetProductCategoryRequest, IQuery<PagedResult<GetProductCategoryResponse>>
    {
    }
}