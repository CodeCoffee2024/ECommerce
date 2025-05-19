using ECommerce.Application.Abstractions.Messaging;
using ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategory;
using ECommerce.Domain.Commons;

namespace ECommerce.Application.CommandQueries.Inventory.ProductCategory.GetProductCategoryDropdown
{
    public class GetProductCategoryDropdownQuery : GetProductCategoryRequest, IQuery<PagedResult<GetProductCategoryDropdownResponse>>
    {
    }
}